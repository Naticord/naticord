import sys
import os
import configparser
import requests
from PyQt5.QtWidgets import (
    QApplication, QLabel, QVBoxLayout, QWidget, QListWidget, QPushButton, 
    QTextEdit, QLineEdit, QHBoxLayout, QProgressBar, QMessageBox, QMenu, 
    QAction, QListWidgetItem, QTabWidget, QFontDialog, QInputDialog
)
from PyQt5.QtCore import QTimer, Qt, pyqtSignal, QVariant
from PyQt5.QtGui import QPixmap, QIcon

class Naticord(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Naticord")
        self.layout = QVBoxLayout()

        self.config = configparser.ConfigParser()
        self.config.read('settings.ini')
        self.mode = self.config['Settings']['mode']

        self.top_bar_layout = QHBoxLayout()
        self.layout.addLayout(self.top_bar_layout)

        self.left_layout = QVBoxLayout()

        self.loading_screen = QWidget()
        self.loading_screen.setFixedSize(200, 150)
        self.loading_layout = QVBoxLayout(self.loading_screen)
        self.loading_label = QLabel("Initializing...")
        self.loading_label.setAlignment(Qt.AlignCenter)
        self.loading_layout.addWidget(self.loading_label)
        self.progress_bar = QProgressBar()
        self.progress_bar.setRange(0, 100)
        self.progress_bar.setTextVisible(False)
        self.loading_layout.addWidget(self.progress_bar)
        self.left_layout.addWidget(self.loading_screen)

        self.user_info_layout = QHBoxLayout()
        self.label_avatar = QLabel()
        self.user_info_layout.addWidget(self.label_avatar)
        self.left_layout.addLayout(self.user_info_layout)


        self.tabs = QTabWidget()

        self.friends_tab = QWidget()
        self.friends_layout = QVBoxLayout(self.friends_tab)
        self.friends_search_input = QLineEdit()
        self.friends_search_input.setPlaceholderText("Search Friends...")
        self.friends_search_input.textChanged.connect(self.search_friends)
        self.friends_layout.addWidget(self.friends_search_input)
        self.friends_list = QListWidget(self.friends_tab)
        self.friends_layout.addWidget(self.friends_list)
        self.friends_list.itemClicked.connect(self.load_dm)
        self.tabs.addTab(self.friends_tab, "Friends")

        self.servers_tab = QWidget()
        self.servers_layout = QVBoxLayout(self.servers_tab)
        self.servers_search_input = QLineEdit()
        self.servers_search_input.setPlaceholderText("Search Servers...")
        self.servers_search_input.textChanged.connect(self.search_servers)
        self.servers_layout.addWidget(self.servers_search_input)
        self.servers_list = QListWidget(self.servers_tab)
        self.servers_layout.addWidget(self.servers_list)
        self.servers_list.itemClicked.connect(self.load_channels)
        self.tabs.addTab(self.servers_tab, "Servers")

        self.left_layout.addWidget(self.tabs)

        self.layout.addLayout(self.left_layout)

        self.right_layout = QVBoxLayout()
        self.messages_text_edit = QTextEdit()
        self.messages_text_edit.setReadOnly(True)
        self.right_layout.addWidget(self.messages_text_edit)

        self.message_input = QLineEdit()
        self.message_input.returnPressed.connect(self.send_message)
        self.right_layout.addWidget(self.message_input)

        self.layout.addLayout(self.right_layout)

        self.setLayout(self.layout)

        self.progress_step = 0

        self.label_avatar.hide()
        self.friends_list.hide()
        self.messages_text_edit.hide()

        self.login_screen = LoginScreen()
        self.login_screen.login_signal.connect(self.load_client)
        self.login_screen.show()

        self.current_channel_id = None
        self.current_server_id = None
        self.resized_once = False

        self.refresh_timer = QTimer(self)
        self.refresh_timer.timeout.connect(self.refresh_dm)
        self.refresh_timer.start(3000)

        self.create_settings_menu()

    def create_settings_menu(self):
        self.settings_button = QPushButton()
        if self.mode == "light":
            self.settings_button.setIcon(QIcon("settings.svg"))
        elif self.mode == "dark":
            self.settings_button.setIcon(QIcon("settings_white.svg"))
        self.settings_button.setFixedSize(30, 30)
        self.settings_button.clicked.connect(self.show_settings_menu)
        self.top_bar_layout.addWidget(self.settings_button, alignment=Qt.AlignRight)

        self.settings_menu = QMenu(self)
        self.light_mode_action = QAction("Light Mode", self)
        self.light_mode_action.triggered.connect(self.set_light_mode)
        self.settings_menu.addAction(self.light_mode_action)

        self.dark_mode_action = QAction("Dark Mode", self)
        self.dark_mode_action.triggered.connect(self.set_dark_mode)
        self.settings_menu.addAction(self.dark_mode_action)

        self.font_selection_action = QAction("Select Font", self)
        self.font_selection_action.triggered.connect(self.select_font)
        self.settings_menu.addAction(self.font_selection_action)

    def show_settings_menu(self):
        self.settings_menu.popup(self.settings_button.mapToGlobal(self.settings_button.rect().bottomRight()))

    def set_light_mode(self):
        self.mode = "light"
        self.apply_style()

    def set_dark_mode(self):
        self.mode = "dark"
        self.apply_style()

    def apply_style(self):
        if self.mode == "light":
            self.setStyleSheet("""
                QWidget {
                    background-color: white;
                    color: black;
                }
                QLineEdit, QTextEdit, QListWidget {
                    border: 1px solid #444;
                }
                QTabBar::tab {
                    background-color: #ddd;
                }
                QTabBar::tab:selected {
                    background-color: #ccc;
                }
            """)
            self.settings_button.setIcon(QIcon("settings.svg"))
        elif self.mode == "dark":
            self.setStyleSheet("""
                QWidget {
                    background-color: #333;
                    color: white;
                }
                QLineEdit, QTextEdit, QListWidget {
                    border: 1px solid #444;
                }
                QTabBar::tab {
                    background-color: #555;
                }
                QTabBar::tab:selected {
                    background-color: #777;
                }
            """)
            self.settings_button.setIcon(QIcon("settings_white.svg"))

        self.config['Settings']['mode'] = self.mode
        with open('settings.ini', 'w') as configfile:
            self.config.write(configfile)

    def load_client(self, token=""):
        token_file_path = os.path.join(os.path.dirname(__file__), "token.txt")
        if not token and os.path.exists(token_file_path):
            with open(token_file_path, "r") as f:
                token = f.read().strip()

        if not token:
            self.login_screen.show()
        else:
            self.token = token
            self.login_screen.hide()
            self.loading_screen.show()
            QTimer.singleShot(1000, lambda: self.authenticate_with_token(token))

    def authenticate_with_token(self, token):
        headers = {"Authorization": f"{token}"}
        response = requests.get("https://discord.com/api/v9/users/@me", headers=headers)
        if response.status_code == 200:
            user_data = response.json()
            self.loading_label.setText(f"Welcome, {user_data.get('username')}!")
            self.progress_bar.setValue(30)
            QTimer.singleShot(1000, lambda: self.update_user_info(user_data, token))
        else:
            QMessageBox.critical(self, "Error", "Authentication failed.")
            self.loading_screen.hide()
            self.load_client()

    def update_user_info(self, user_data, token):
        avatar_url = f"https://cdn.discordapp.com/avatars/{user_data.get('id')}/{user_data.get('avatar')}.png"

        self.loading_label.setText("Loading profile picture...")
        self.progress_bar.setValue(50)
        avatar_data = requests.get(avatar_url).content

        pixmap = QPixmap()
        pixmap.loadFromData(avatar_data)
        pixmap = pixmap.scaled(48, 48, Qt.KeepAspectRatio)
        self.label_avatar.setPixmap(pixmap)
        self.label_avatar.show()

        self.loading_label.setText("Loading friend and server lists...")
        self.progress_bar.setValue(80)
        QTimer.singleShot(1000, lambda: self.populate_friends_and_servers(token))


    def populate_friends_and_servers(self, token):
        headers = {"Authorization": f"{token}"}
        response = requests.get("https://discord.com/api/v9/users/@me/relationships", headers=headers)
        if response.status_code == 200:
            friends_data = response.json()
            for friend in friends_data:
                friend_name = friend.get("user", {}).get("username")
                self.friends_list.addItem(friend_name)

        response = requests.get("https://discord.com/api/v9/users/@me/guilds", headers=headers)
        if response.status_code == 200:
            servers_data = response.json()
            for server in servers_data:
                server_name = server.get("name")
                self.servers_list.addItem(server_name)

        self.progress_bar.setValue(100)
        self.loading_label.setText("Loading complete!")
        self.loading_screen.hide()
        self.friends_list.show()
        self.servers_list.show()

    def load_dm(self, item):
        friend_name = item.text()
        headers = {"Authorization": f"{self.token}"}
        response = requests.get("https://discord.com/api/v9/users/@me/channels", headers=headers)
        if response.status_code == 200:
            channels_data = response.json()
            for channel in channels_data:
                if channel.get("type") == 1:
                    recipients = channel.get("recipients", [])
                    if len(recipients) == 1 and recipients[0].get("username") == friend_name:
                        channel_id = channel.get("id")
                        if channel_id != self.current_channel_id:
                            messages = self.fetch_messages(channel_id)
                            if messages:
                                self.display_messages(messages)
                                self.messages_text_edit.show()
                                self.message_input.show()
                                if not getattr(self, 'resized_once', False):
                                    self.resize_once()
                                    self.resized_once = True
                                self.current_channel_id = channel_id
                            else:
                                QMessageBox.warning(self, "Error", f"Failed to fetch messages for {friend_name}.")
                        break
        else:
            QMessageBox.warning(self, "Error", "Failed to fetch DM channels.")

    def refresh_dm(self):
        if self.current_channel_id is not None:
            messages = self.fetch_messages(self.current_channel_id)
            if messages:
                self.display_messages(messages)

    def load_channels(self, item):
        server_name = item.text()
        headers = {"Authorization": f"{self.token}"}
        response = requests.get(f"https://discord.com/api/v9/users/@me/guilds", headers=headers)
        if response.status_code == 200:
            servers_data = response.json()
            for server in servers_data:
                if server.get("name") == server_name:
                    self.current_server_id = server.get("id")
                    response = requests.get(f"https://discord.com/api/v9/guilds/{self.current_server_id}/channels", headers=headers)
                    if response.status_code == 200:
                        channels_data = response.json()
                        self.servers_list.clear()
                        back_item = QListWidgetItem("⮜ Go Back")
                        back_item.setData(Qt.UserRole, "back")
                        self.servers_list.addItem(back_item)
                        for channel in channels_data:
                            channel_name = channel.get("name")
                            self.servers_list.addItem(channel_name)
                            self.servers_list.item(self.servers_list.count() - 1).setData(Qt.UserRole, channel.get("id"))
                        self.servers_list.itemClicked.connect(self.load_messages)
                    break

    def load_messages(self, item):
        channel_id = item.data(Qt.UserRole)
        if channel_id == "back":
            self.servers_list.show()
            self.friends_list.clear()
            return
        if channel_id != self.current_channel_id:
            messages = self.fetch_messages(channel_id)
            if messages:
                self.display_messages(messages)
                self.messages_text_edit.show()
                self.message_input.show()
                if not getattr(self, 'resized_once', False):
                    self.resize_once()
                    self.resized_once = True
                self.current_channel_id = channel_id
            else:
                QMessageBox.warning(self, "Error", "Failed to fetch messages.")

    def resize_once(self):
        new_width = self.calculate_new_width()
        self.resize(new_width, self.height())

    def calculate_new_width(self):
        content_width = self.messages_text_edit.sizeHint().width() + 30
        return max(self.width(), content_width)

    def fetch_messages(self, channel_id):
        headers = {"Authorization": f"{self.token}"}
        response = requests.get(f"https://discord.com/api/v9/channels/{channel_id}/messages", headers=headers, params={"limit": 5})
        if response.status_code == 200:
            messages_data = response.json()
            return messages_data
        else:
            return None

    def display_messages(self, messages):
        self.messages_text_edit.clear()
        for message in reversed(messages):
            author = message.get("author", {}).get("username")
            content = message.get("content")

            menu = QMenu()
            edit_action = QAction("Edit")
            edit_action.triggered.connect(lambda _, msg_id=message.get("id"): self.edit_message(msg_id))
            menu.addAction(edit_action)

            delete_action = QAction("Delete")
            delete_action.triggered.connect(lambda _, msg_id=message.get("id"): self.delete_message(msg_id))
            menu.addAction(delete_action)

            reply_action = QAction("Reply")
            reply_action.triggered.connect(lambda _, author=author: self.reply_to_author(author))
            menu.addAction(reply_action)
            
            message_item = QListWidgetItem(f"{author}: {content}")
            message_item.setIcon(QIcon("hamburger.svg"))

            message_item.setData(Qt.UserRole, QVariant(menu))
            self.messages_text_edit.append(f"{author}: {content}")

    def edit_message(self, message_id):
        message_text = self.get_message_text_by_id(message_id)
        if message_text:
            new_message_text, ok = QInputDialog.getText(self, "Edit Message", "Enter the new message:", QLineEdit.Normal, message_text)
            if ok:
                url = f"https://discord.com/api/v9/channels/{self.current_channel_id}/messages/{message_id}"
                headers = {"Authorization": f"{self.token}", "Content-Type": "application/json"}
                data = {"content": new_message_text}
                response = requests.patch(url, headers=headers, json=data)
                if response.status_code == 200:
                    QMessageBox.information(self, "Success", "Message edited successfully.")
                    self.refresh_messages()
                else:
                    QMessageBox.warning(self, "Error", "Failed to edit message.")

    def delete_message(self, message_id):
        confirm = QMessageBox.question(self, "Delete Message", "Are you sure you want to delete this message?", QMessageBox.Yes | QMessageBox.No)
        if confirm == QMessageBox.Yes:
            url = f"https://discord.com/api/v9/channels/{self.current_channel_id}/messages/{message_id}"
            headers = {"Authorization": f"{self.token}"}
            response = requests.delete(url, headers=headers)
            if response.status_code == 204:
                QMessageBox.information(self, "Success", "Message deleted successfully.")
                self.refresh_messages()
            else:
                QMessageBox.warning(self, "Error", "Failed to delete message.")

    def refresh_messages(self):
        if self.current_channel_id:
            messages = self.fetch_messages(self.current_channel_id)
            if messages:
                self.display_messages(messages)

    def reply_to_author(self, author):
        self.message_input.setText(f"@{author} ")
        self.message_input.setFocus()

    def send_message(self):
        if self.message_input.text():
            message = self.message_input.text()
            headers = {"Authorization": f"{self.token}", "Content-Type": "application/json"}
            data = {"content": message}
            url = f"https://discord.com/api/v9/channels/{self.current_channel_id}/messages"
            response = requests.post(url, headers=headers, json=data)
            if response.status_code == 200:
                self.message_input.clear()
                self.refresh_messages()
            else:
                QMessageBox.warning(self, "Error", "Failed to send message.")

    def search_friends(self, text):
        items = self.friends_list.findItems("*", Qt.MatchWrap | Qt.MatchWildcard)
        for item in items:
            if text.lower() not in item.text().lower():
                item.setHidden(True)
            else:
                item.setHidden(False)

    def search_servers(self, text):
        items = self.servers_list.findItems("*", Qt.MatchWrap | Qt.MatchWildcard)
        for item in items:
            if text.lower() not in item.text().lower():
                item.setHidden(True)
            else:
                item.setHidden(False)

    def select_font(self):
        font, ok = QFontDialog.getFont()
        if ok:
            self.messages_text_edit.setFont(font)

class LoginScreen(QWidget):
    login_signal = pyqtSignal(str)

    def __init__(self):
        super().__init__()
        self.setWindowTitle("Login")
        layout = QVBoxLayout()
        self.token_input = QLineEdit()
        self.token_input.setPlaceholderText("Enter your token")
        layout.addWidget(self.token_input)
        login_button = QPushButton("Login")
        login_button.clicked.connect(self.login)
        layout.addWidget(login_button)
        self.setLayout(layout)

    def login(self):
        token = self.token_input.text().strip()
        if token:
            self.login_signal.emit(token)

if __name__ == "__main__":
    app = QApplication(sys.argv)
    window = Naticord()
    window.show()
    sys.exit(app.exec_())

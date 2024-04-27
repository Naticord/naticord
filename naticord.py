import sys
from PyQt5.QtWidgets import QApplication, QLabel, QVBoxLayout, QWidget, QListWidget, QPushButton, QTextEdit, QLineEdit, QHBoxLayout, QProgressBar, QMessageBox, QMenu, QAction
from PyQt5.QtCore import QTimer, Qt, pyqtSignal
from PyQt5.QtGui import QPixmap, QIcon, QPalette, QColor
import requests
import os
import configparser

class Naticord(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Naticord")
        self.layout = QHBoxLayout()

        self.config = configparser.ConfigParser()
        self.config.read('settings.ini')
        self.mode = self.config['Settings']['mode']

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

        self.user_info_layout = QVBoxLayout()
        self.label_username = QLabel()
        self.user_info_layout.addWidget(self.label_username, alignment=Qt.AlignCenter)
        self.label_avatar = QLabel()
        self.user_info_layout.addWidget(self.label_avatar, alignment=Qt.AlignCenter)
        self.left_layout.addLayout(self.user_info_layout)

        self.friends_list = QListWidget()
        self.friends_list.itemClicked.connect(self.load_dm)
        self.left_layout.addWidget(self.friends_list)

        self.layout.addLayout(self.left_layout)

        self.right_layout = QVBoxLayout()
        self.messages_text_edit = QTextEdit()
        self.messages_text_edit.setReadOnly(True)
        self.right_layout.addWidget(self.messages_text_edit)
        self.messages_text_edit.hide()

        self.message_input = QLineEdit()
        self.message_input.returnPressed.connect(self.send_message)
        self.right_layout.addWidget(self.message_input)
        self.message_input.hide()

        self.layout.addLayout(self.right_layout)

        self.setLayout(self.layout)

        self.progress_step = 0

        self.label_avatar.hide()
        self.label_username.hide()
        self.friends_list.hide()
        self.messages_text_edit.hide()

        self.login_screen = LoginScreen()
        self.login_screen.login_signal.connect(self.load_client)
        self.login_screen.show()

        self.current_channel_id = None
        self.resized_once = False

        self.refresh_timer = QTimer(self)
        self.refresh_timer.timeout.connect(self.refresh_dm)
        self.refresh_timer.start(3000)

        self.create_settings_menu()

    def create_settings_menu(self):
        self.settings_button = QPushButton()
        self.settings_button.setIcon(QIcon("settings.svg"))
        self.settings_button.setFixedSize(30, 30)
        self.settings_button.clicked.connect(self.show_settings_menu)
        self.layout.addWidget(self.settings_button)

        self.settings_menu = QMenu(self)
        self.light_mode_action = QAction("Light Mode", self)
        self.light_mode_action.triggered.connect(self.set_light_mode)
        self.settings_menu.addAction(self.light_mode_action)

        self.dark_mode_action = QAction("Dark Mode", self)
        self.dark_mode_action.triggered.connect(self.set_dark_mode)
        self.settings_menu.addAction(self.dark_mode_action)

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
            self.setStyleSheet("")
            self.setStyleSheet("background-color: white; color: black;")
        elif self.mode == "dark":
            self.setStyleSheet("")
            self.setStyleSheet("background-color: black; color: white;")

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
        username = user_data.get("username")
        avatar_url = f"https://cdn.discordapp.com/avatars/{user_data.get('id')}/{user_data.get('avatar')}.png"

        self.label_username.setText(username)
        self.label_username.show()

        self.loading_label.setText("Loading profile picture...")
        self.progress_bar.setValue(50)
        avatar_data = requests.get(avatar_url).content

        pixmap = QPixmap()
        pixmap.loadFromData(avatar_data)

        self.label_avatar.setPixmap(pixmap)
        self.label_avatar.show()

        self.loading_label.setText("Loading friend list...")
        self.progress_bar.setValue(80)
        QTimer.singleShot(1000, lambda: self.populate_friends_list(token))

    def populate_friends_list(self, token):
        headers = {"Authorization": f"{token}"}
        response = requests.get("https://discord.com/api/v9/users/@me/relationships", headers=headers)
        if response.status_code == 200:
            friends_data = response.json()
            for friend in friends_data:
                friend_name = friend.get("user", {}).get("username")
                self.friends_list.addItem(friend_name)

            self.progress_bar.setValue(100)
            self.loading_label.setText("Loading complete!")
            self.loading_screen.hide()
            self.friends_list.show()
            self.label_avatar.show()
        else:
            print("Failed to fetch friends list.")

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
                                if not getattr(self, 'resized_once', False):
                                    self.resize_once()
                                    self.resized_once = True
                                self.current_channel_id = channel_id
                            else:
                                QMessageBox.warning(self, "Error", f"Failed to fetch messages for {friend_name}.")
                        break
        else:
            QMessageBox.warning(self, "Error", "Failed to fetch DM channels.")

    def resize_once(self):
        new_width = self.calculate_new_width()
        self.resize(new_width, self.height())

    def calculate_new_width(self):
        content_width = self.messages_text_edit.sizeHint().width() + 30
        return max(self.width(), content_width)

    def fetch_messages(self, channel_id):
        headers = {"Authorization": f"{self.token}"}
        response = requests.get(f"https://discord.com/api/v9/channels/{channel_id}/messages", headers=headers, params={"limit": 20})
        if response.status_code == 200:
            messages_data = response.json()
            return messages_data
        else:
            return None

    def display_messages(self, messages):
        formatted_messages = ""
        for message in reversed(messages):
            author = message.get("author", {}).get("username")
            content = message.get("content")
            formatted_messages += f"{author}: {content}\n"
        self.messages_text_edit.setPlainText(formatted_messages)

    def send_message(self):
        if self.current_channel_id is not None:
            message_content = self.message_input.text().strip()
            if message_content:
                headers = {"Authorization": f"{self.token}", "Content-Type": "application/json"}
                data = {"content": message_content}
                response = requests.post(f"https://discord.com/api/v9/channels/{self.current_channel_id}/messages", headers=headers, json=data)
                if response.status_code == 200:
                    print("Message sent successfully!")
                    self.message_input.clear()
                else:
                    print("Failed to send message.")
            else:
                print("Message content is empty.")

    def refresh_dm(self):
        if self.current_channel_id is not None:
            messages = self.fetch_messages(self.current_channel_id)
            if messages:
                self.display_messages(messages)

class LoginScreen(QWidget):
    login_signal = pyqtSignal(str)

    def __init__(self):
        super().__init__()
        self.setWindowTitle("Login")
        self.layout = QVBoxLayout()

        self.token_label = QLabel("Token:")
        self.layout.addWidget(self.token_label)

        self.token_edit = QLineEdit()
        self.layout.addWidget(self.token_edit)

        self.login_button = QPushButton("Login")
        self.login_button.clicked.connect(self.on_login_clicked)
        self.layout.addWidget(self.login_button)

        self.setLayout(self.layout)

    def on_login_clicked(self):
        token = self.token_edit.text().strip()
        if token:
            token_file_path = os.path.join(os.path.dirname(__file__), "token.txt")
            with open(token_file_path, "w") as f:
                f.write(token)
            self.login_signal.emit(token)
        else:
            QMessageBox.critical(self, "Error", "Please enter a token.")

def main():
    app = QApplication(sys.argv)
    client = Naticord()
    client.show()
    sys.exit(app.exec_())

if __name__ == "__main__":
    main()
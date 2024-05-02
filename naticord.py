import sys
import requests
import configparser
import os
from PyQt5.QtWidgets import QApplication, QWidget, QVBoxLayout, QHBoxLayout, QLabel, QListWidget, QTextEdit, QLineEdit, QPushButton, QTabWidget, QMessageBox, QDialog, QListWidgetItem
from PyQt5.QtCore import Qt, QTimer

class LoginScreen(QDialog):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Login")
        self.layout = QVBoxLayout()

        self.token_input = QLineEdit()
        self.token_input.setPlaceholderText("Enter your Discord token...")  # set placeholder text for token input
        self.layout.addWidget(self.token_input)

        self.login_button = QPushButton("Login")
        self.login_button.clicked.connect(self.login)
        self.layout.addWidget(self.login_button)

        self.setLayout(self.layout)

    def login(self):
        token = self.token_input.text().strip()
        if token:
            config = configparser.ConfigParser()
            config['Auth'] = {'Token': token}
            config_path = os.path.join(os.path.expanduser("~"), "config.ini")  # save config.ini to user's home directory
            with open(config_path, 'w') as configfile:
                config.write(configfile)
            self.accept()
        else:
            QMessageBox.critical(self, "Error", "Please enter a valid Discord token.")

class Naticord(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Naticord")
        self.layout = QHBoxLayout()
        self.setLayout(self.layout)

        # left panel containing user info, friends, and servers tabs
        self.left_panel = QWidget()
        self.left_panel_layout = QVBoxLayout()
        self.left_panel.setLayout(self.left_panel_layout)

        # initialize friends and servers tabs
        self.friends_tab = QWidget()
        self.servers_tab = QWidget()

        # initialize QTabWidget to contain friends and servers tabs
        self.tabs = QTabWidget()
        self.tabs.addTab(self.friends_tab, "Friends")
        self.tabs.addTab(self.servers_tab, "Servers")

        # layouts for friends and servers tabs
        self.friends_layout = QVBoxLayout(self.friends_tab)
        self.servers_layout = QVBoxLayout(self.servers_tab)

        # labels for user info, friends, and servers
        self.user_info_label = QLabel("User Info")
        self.friends_label = QLabel("Friends")
        self.servers_label = QLabel("Servers")

        # list widgets to display friends and servers
        self.friends_list = QListWidget()
        self.servers_list = QListWidget()

        # add labels and list widgets to layouts
        self.friends_layout.addWidget(self.friends_label)
        self.friends_layout.addWidget(self.friends_list)

        self.servers_layout.addWidget(self.servers_label)
        self.servers_layout.addWidget(self.servers_list)

        # add user info label and tabs
        self.left_panel_layout.addWidget(self.user_info_label)
        self.left_panel_layout.addWidget(self.tabs)

        # add left panel to main layout
        self.layout.addWidget(self.left_panel)

        # right panel containing messages display and message input
        self.right_panel = QWidget()
        self.right_panel_layout = QVBoxLayout()
        self.right_panel.setLayout(self.right_panel_layout)

        # label for messages
        self.messages_label = QLabel("Messages")
        self.right_panel_layout.addWidget(self.messages_label)

        # text edit to display messages
        self.messages_text_edit = QTextEdit()
        self.right_panel_layout.addWidget(self.messages_text_edit)

        # line edit for typing messages
        self.message_input = QLineEdit()
        self.message_input.returnPressed.connect(self.send_message)
        self.right_panel_layout.addWidget(self.message_input)

        # add right panel to main layout
        self.layout.addWidget(self.right_panel)

        # search bar for filtering lists
        self.search_bar = QLineEdit()
        self.search_bar.setPlaceholderText("Search...")
        self.search_bar.textChanged.connect(self.filter_lists)

        # retrieve Discord token from config.ini
        self.token = self.get_token()
        if not self.token:
            self.show_login_screen()
        else:
            self.load_data()

        # start timer to refresh messages every 3 seconds
        self.refresh_timer = QTimer(self)
        self.refresh_timer.timeout.connect(self.refresh_messages)
        self.refresh_timer.start(3000)

    def show_login_screen(self):
        login_screen = LoginScreen()
        if login_screen.exec_() == QDialog.Accepted:
            self.token = self.get_token()
            self.load_data()
        else:
            sys.exit()

    def get_token(self):
        config = configparser.ConfigParser()
        config_path = os.path.join(os.path.expanduser("~"), "config.ini")  # looks for config.ini in user dir
        config.read(config_path)
        return config['Auth']['Token'] if 'Auth' in config and 'Token' in config['Auth'] else None
    
    def load_data(self):
        self.load_user_info()
        self.load_friends()
        self.load_servers()

    def load_user_info(self):
        headers = {"Authorization": f"{self.token}"}
        response = requests.get("https://discord.com/api/v9/users/@me", headers=headers)
        if response.status_code == 200:
            user_data = response.json()
            self.user_info_label.setText(f"Welcome, {user_data.get('username')}")
        else:
            QMessageBox.warning(self, "Error", "Failed to fetch user information.")

    def load_friends(self):
        headers = {"Authorization": f"{self.token}"}
        response = requests.get("https://discord.com/api/v9/users/@me/channels", headers=headers)
        if response.status_code == 200:
            channels_data = response.json()
            self.original_friends = []  # Store original items for filtering
            for channel in channels_data:
                recipients = channel.get("recipients", [])
                if recipients:
                    friend_name = recipients[0].get("username", "Unknown")
                    item = QListWidgetItem(friend_name)
                    item.setData(Qt.UserRole, channel.get("id"))
                    self.friends_list.addItem(item)
                    self.original_friends.append(item)  # Add item to original list
            self.friends_list.itemDoubleClicked.connect(self.load_channel_messages)
        else:
            QMessageBox.warning(self, "Error", "Failed to fetch friends.")

    def load_servers(self):
        headers = {"Authorization": f"{self.token}"}
        response = requests.get("https://discord.com/api/v9/users/@me/guilds", headers=headers)
        if response.status_code == 200:
            servers_data = response.json()
            self.original_servers = []  # Store original items for filtering
            for server in servers_data:
                server_name = server.get("name")
                item = QListWidgetItem(server_name)
                item.setData(Qt.UserRole, server.get("id"))
                self.servers_list.addItem(item)
                self.original_servers.append(item)  # Add item to original list
            self.servers_list.itemDoubleClicked.connect(self.load_server_channels)
        else:
            QMessageBox.warning(self, "Error", "Failed to fetch servers.")

    def load_server_channels(self, item):
        self.messages_text_edit.clear()
        channel_id = item.data(Qt.UserRole)
        if channel_id:
            headers = {"Authorization": f"{self.token}"}
            response = requests.get(f"https://discord.com/api/v9/guilds/{channel_id}/channels", headers=headers)
            if response.status_code == 200:
                channels_data = response.json()
                text_channels = [channel for channel in channels_data if channel["type"] == 0]
                if text_channels:
                    channel = text_channels[0]
                    channel_id = channel["id"]
                    self.load_channel_messages(QListWidgetItem(channel["name"], self.servers_list), channel_id)
                else:
                    QMessageBox.warning(self, "Error", "No text channels found in the server.")
            else:
                QMessageBox.warning(self, "Error", "Failed to fetch server channels.")

    def load_channel_messages(self, item, channel_id):
        if channel_id:
            headers = {"Authorization": f"{self.token}"}
            response = requests.get(f"https://discord.com/api/v9/channels/{channel_id}/messages", headers=headers, params={"limit": 20})
            if response.status_code == 200:
                messages_data = response.json()
                self.display_messages(messages_data)
            else:
                QMessageBox.warning(self, "Error", "Failed to fetch messages.")
    
    def display_messages(self, messages):
        self.messages_text_edit.clear()
        for message in reversed(messages):
            author = message.get("author", {}).get("username", "Unknown")
            content = self.format_pings(message.get("content", ""))
            self.messages_text_edit.append(f"{author}: {content}")

    def format_pings(self, content):
        headers = {"Authorization": f"{self.token}"}
        while "<@!" in content:
            start_index = content.index("<@!")
            end_index = content.index(">", start_index)
            user_id = content[start_index + 3:end_index]
            username = self.get_username(user_id)
            if username:
                content = content.replace(f"<@!{user_id}>", f"<span style=\"color: blue;\">@{username}</span>")
            else:
                content = content.replace(f"<@!{user_id}>", f"<@!{user_id}>")
        while "<@" in content:
            start_index = content.index("<@")
            end_index = content.index(">", start_index)
            user_id = content[start_index + 2:end_index]
            username = self.get_username(user_id)
            if username:
                content = content.replace(f"<@{user_id}>", f"<span style=\"color: blue;\">@{username}</span>")
            else:
                content = content.replace(f"<@{user_id}>", f"<@{user_id}>")
        return content

    def get_username(self, user_id):
        headers = {"Authorization": f"{self.token}"}
        response = requests.get(f"https://discord.com/api/v9/users/{user_id}", headers=headers)
        if response.status_code == 200:
            user_data = response.json()
            return user_data.get('username')
        else:
            return None

    def send_message(self):
        message = self.message_input.text()
        selected_tab_index = self.tabs.currentIndex()
        
        if selected_tab_index == 0:
            selected_item = self.friends_list.currentItem()
            if selected_item:
                recipient_id = selected_item.data(Qt.UserRole)
                self.send_direct_message(recipient_id, message)
        elif selected_tab_index == 1:
            selected_item = self.servers_list.currentItem()
            if selected_item:
                channel_id = selected_item.data(Qt.UserRole)
                self.send_channel_message(channel_id, message)

    def send_direct_message(self, recipient_id, message):
        headers = {"Authorization": f"{self.token}", "Content-Type": "application/json"}
        payload = {"content": message}
        response = requests.post(f"https://discord.com/api/v9/channels/{recipient_id}/messages", headers=headers, json=payload)
        if response.status_code != 200:
            QMessageBox.warning(self, "Error", "Failed to send message.")

    def send_channel_message(self, channel_id, message):
        headers = {"Authorization": f"{self.token}", "Content-Type": "application/json"}
        payload = {"content": message}
        response = requests.post(f"https://discord.com/api/v9/channels/{channel_id}/messages", headers=headers, json=payload)
        if response.status_code != 200:
            QMessageBox.warning(self, "Error", "Failed to send message.")

    def refresh_messages(self):
        selected_tab_index = self.tabs.currentIndex()
        selected_item = None
        if selected_tab_index == 0:
            selected_item = self.friends_list.currentItem()
        elif selected_tab_index == 1:
            selected_item = self.servers_list.currentItem()
        if selected_item:
            if selected_tab_index == 0:
                channel_id = selected_item.data(Qt.UserRole)
                self.load_channel_messages(selected_item, channel_id)
            elif selected_tab_index == 1:
                self.load_server_channels(selected_item)

    def filter_lists(self):
        # Get the current tab index
        selected_tab_index = self.tabs.currentIndex()
        # Clear the respective list widget
        if selected_tab_index == 0:
            list_widget = self.friends_list
            original_items = self.original_friends.copy()  # Make a copy of the original list
        elif selected_tab_index == 1:
            list_widget = self.servers_list
            original_items = self.original_servers.copy()  # Make a copy of the original list
        list_widget.clear()
        # Get the text from the search bar
        search_text = self.search_bar.text().strip().lower()
        # Iterate through the original items and add matching items to the filtered list
        for item in original_items:
            if search_text in item.text().lower():
                list_widget.addItem(QListWidgetItem(item.text(), list_widget))  # Add a copy of the item

if __name__ == "__main__":
    app = QApplication(sys.argv)
    client = Naticord()
    client.show()
    sys.exit(app.exec_())

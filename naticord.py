# import necessary modules
import sys
import asyncio
import aiohttp
import configparser
import os
from PyQt5.QtWidgets import QApplication, QWidget, QVBoxLayout, QHBoxLayout, QLabel, QListWidget, QTextEdit, QLineEdit, QPushButton, QTabWidget, QMessageBox, QDialog, QListWidgetItem
from PyQt5.QtCore import Qt, QTimer

# define a dialog for the login screen
class LoginScreen(QDialog):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Login")
        self.layout = QVBoxLayout()

        # create input field for discord token
        self.token_input = QLineEdit()
        self.token_input.setPlaceholderText("Enter your token...")  
        self.layout.addWidget(self.token_input)

        # create login button and connect it to login function
        self.login_button = QPushButton("Login")
        self.login_button.clicked.connect(self.login)
        self.layout.addWidget(self.login_button)

        self.setLayout(self.layout)

    # function to handle login button click
    def login(self):
        token = self.token_input.text().strip()
        if token:
            # if token is provided, save it to config file
            config = configparser.ConfigParser()
            config['auth'] = {'token': token}
            config_path = os.path.join(os.path.expanduser("~"), "config.ini")
            with open(config_path, 'w') as configfile:
                config.write(configfile)
            self.accept()  # close the login dialog
        else:
            QMessageBox.critical(self, "Error", "Please enter a valid discord token.")

# main application window
class Naticord(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Naticord")
        self.layout = QHBoxLayout()
        self.setLayout(self.layout)

        # left panel containing user info, friends list, and servers list
        self.left_panel = QWidget()
        self.left_panel_layout = QVBoxLayout()
        self.left_panel.setLayout(self.left_panel_layout)

        # tabs for friends and servers
        self.friends_tab = QWidget()
        self.servers_tab = QWidget()

        # tab widget to switch between friends and servers
        self.tabs = QTabWidget()
        self.tabs.addTab(self.friends_tab, "Friends")
        self.tabs.addTab(self.servers_tab, "Servers")

        # layouts for friends and servers tabs
        self.friends_layout = QVBoxLayout(self.friends_tab)
        self.servers_layout = QVBoxLayout(self.servers_tab)

        # labels for user info, friends, and servers
        self.user_info_label = QLabel("User info")
        self.friends_label = QLabel("Friends")
        self.servers_label = QLabel("Servers")

        # lists to display friends and servers
        self.friends_list = QListWidget()
        self.servers_list = QListWidget()

        # add labels and lists to respective layouts
        self.friends_layout.addWidget(self.friends_label)
        self.friends_layout.addWidget(self.friends_list)

        self.servers_layout.addWidget(self.servers_label)
        self.servers_layout.addWidget(self.servers_list)

        # add user info label and tabs to left panel layout
        self.left_panel_layout.addWidget(self.user_info_label)
        self.left_panel_layout.addWidget(self.tabs)

        self.layout.addWidget(self.left_panel)

        # right panel containing messages and message input field
        self.right_panel = QWidget()
        self.right_panel_layout = QVBoxLayout()
        self.right_panel.setLayout(self.right_panel_layout)

        # label for messages
        self.messages_label = QLabel("Messages")
        self.right_panel_layout.addWidget(self.messages_label)

        # text edit widget to display messages
        self.messages_text_edit = QTextEdit()
        self.messages_text_edit.setReadOnly(True)  # make it read-only
        self.right_panel_layout.addWidget(self.messages_text_edit)

        # input field to send messages
        self.message_input = QLineEdit()
        self.message_input.returnPressed.connect(self.send_message)
        self.right_panel_layout.addWidget(self.message_input)

        self.layout.addWidget(self.right_panel)

        # check if token exists, if not, show login screen
        self.token = self.get_token()
        if not self.token:
            self.show_login_screen()
        else:
            self.load_data()

        # timer to periodically refresh messages
        self.refresh_timer = QTimer(self)
        self.refresh_timer.timeout.connect(self.refresh_messages)
        self.refresh_timer.start(3000)

    # function to display login screen
    def show_login_screen(self):
        login_screen = LoginScreen()
        if login_screen.exec_() == QDialog.Accepted:
            self.token = self.get_token()
            self.load_data()
        else:
            sys.exit()

    # function to retrieve token from config file
    def get_token(self):
        config = configparser.ConfigParser()
        config_path = os.path.join(os.path.expanduser("~"), "config.ini")
        if os.path.exists(config_path):  # check if config file exists
            config.read(config_path)
            if 'auth' in config and 'token' in config['auth']:  # check if token exists in config
                return config['auth']['token']
        return None
    
    # function to load user info, friends, and servers
    def load_data(self):
        asyncio.run(self.load_user_info())
        asyncio.run(self.load_friends())
        asyncio.run(self.load_servers())

    # function to load user info
    async def load_user_info(self):
        headers = {"authorization": f"{self.token}"}
        async with aiohttp.ClientSession() as session:
            async with session.get("https://discord.com/api/v9/users/@me", headers=headers) as response:
                if response.status == 200:
                    user_data = await response.json()
                    self.user_info_label.setText(f"welcome, {user_data.get('username')}")
                else:
                    QMessageBox.warning(self, "Error", "Failed to fetch user information.")

    # function to load friends
    async def load_friends(self):
        headers = {"authorization": f"{self.token}"}
        async with aiohttp.ClientSession() as session:
            async with session.get("https://discord.com/api/v9/users/@me/channels", headers=headers) as response:
                if response.status == 200:
                    channels_data = await response.json()
                    existing_friends = set()  # store existing friend IDs to avoid duplicates
                    for channel in channels_data:
                        recipients = channel.get("recipients", [])
                        if recipients:
                            friend_id = recipients[0].get("id")
                            if friend_id not in existing_friends:  # check for duplicates
                                friend_name = recipients[0].get("username", "unknown")
                                item = QListWidgetItem(friend_name)
                                item.setData(Qt.UserRole, channel.get("id"))
                                self.friends_list.addItem(item)
                                existing_friends.add(friend_id)
                    self.friends_list.itemDoubleClicked.connect(self.load_direct_messages)
                else:
                    QMessageBox.warning(self, "Error", "Failed to fetch friends.")

    # function to load servers
    async def load_servers(self):
        headers = {"authorization": f"{self.token}"}
        async with aiohttp.ClientSession() as session:
            async with session.get("https://discord.com/api/v9/users/@me/guilds", headers=headers) as response:
                if response.status == 200:
                    servers_data = await response.json()
                    for server in servers_data:
                        server_name = server.get("name")
                        item = QListWidgetItem(server_name)
                        item.setData(Qt.UserRole, server.get("id"))
                        self.servers_list.addItem(item)
                    self.servers_list.itemClicked.connect(self.load_server_channels)  
                else:
                    QMessageBox.warning(self, "Error", "Failed to fetch servers.")

    # function to load direct messages
    async def load_direct_messages(self, item):
        self.messages_text_edit.clear()
        recipient_id = item.data(Qt.UserRole)
        if recipient_id:
            headers = {"authorization": f"{self.token}"}
            async with aiohttp.ClientSession() as session:
                async with session.get(f"https://discord.com/api/v9/channels/{recipient_id}/messages", headers=headers, params={"limit": 20}) as response:
                    if response.status == 200:
                        messages_data = await response.json()
                        self.display_messages(messages_data)
                    else:
                        QMessageBox.warning(self, "Error", "Failed to fetch messages")

    # function to load server channels
    async def load_server_channels(self, item):
        self.messages_text_edit.clear()
        server_id = item.data(Qt.UserRole)
        if server_id:
            headers = {"authorization": f"{self.token}"}
            async with aiohttp.ClientSession() as session:
                async with session.get(f"https://discord.com/api/v9/guilds/{server_id}/channels", headers=headers) as response:
                    if response.status == 200:
                        channels_data = await response.json()
                        channel_names = ["back"]  
                        channel_names += [channel.get("name") for channel in channels_data]
                        self.servers_list.clear()
                        self.servers_list.addItems(channel_names)
                        self.servers_list.itemClicked.connect(self.handle_channel_click)  
                    else:
                        QMessageBox.warning(self, "Error", "Failed to fetch server channels.")

    # function to handle channel click
    async def handle_channel_click(self, item):
        if item.text() == "back":
            await self.load_servers()
        else:
            channel_name = item.text()
            selected_server_item = self.servers_list.currentItem()
            if selected_server_item:
                server_id = selected_server_item.data(Qt.UserRole)
                if server_id:
                    headers = {"authorization": f"{self.token}"}
                    async with aiohttp.ClientSession() as session:
                        async with session.get(f"https://discord.com/api/v9/guilds/{server_id}/channels", headers=headers) as response:
                            if response.status == 200:
                                channels_data = await response.json()
                                channel_id = None
                                for channel in channels_data:
                                    if channel.get("name") == channel_name:
                                        channel_id = channel.get("id")
                                        break
                                if channel_id:
                                    async with session.get(f"https://discord.com/api/v9/channels/{channel_id}/messages", headers=headers, params={"limit": 20}) as response:
                                        if response.status == 200:
                                            messages_data = await response.json()
                                            self.display_messages(messages_data)
                                        else:
                                            QMessageBox.warning(self, "Error", "Failed to fetch messages.")
                                else:
                                    QMessageBox.warning(self, "Error", "Channel ID not found.")
                            else:
                                QMessageBox.warning(self, "Error", "Failed to fetch server channels.")

    # function to display messages
    def display_messages(self, messages):
        self.messages_text_edit.clear()
        for message in reversed(messages):
            author = message.get("author", {}).get("username", "unknown")
            content = self.format_pings(message.get("content", ""))
            self.messages_text_edit.append(f"<b>{author}:</b> {content}")

    # function to format ping mentions
    def format_pings(self, content):
        headers = {"authorization": f"{self.token}"}
        while "<@!" in content:
            start_index = content.index("<@!")
            end_index = content.index(">", start_index)
            user_id = content[start_index + 3:end_index]
            username = self.get_username(user_id)
            if username:
                content = content.replace(f"<@!{user_id}>", f'<span style="color: blue;">@{username}</span>')
            else:
                content = content.replace(f"<@!{user_id}>", f"<@!{user_id}>")
        while "<@" in content:
            start_index = content.index("<@")
            end_index = content.index(">", start_index)
            user_id = content[start_index + 2:end_index]
            username = self.get_username(user_id)
            if username:
                content = content.replace(f"<@{user_id}>", f'<span style="color: blue;">@{username}</span>')
            else:
                content = content.replace(f"<@{user_id}>", f"<@{user_id}>")
        return content

    # function to get username
    async def get_username(self, user_id):
        headers = {"authorization": f"{self.token}"}
        async with aiohttp.ClientSession() as session:
            async with session.get(f"https://discord.com/api/v9/users/{user_id}", headers=headers) as response:
                if response.status == 200:
                    user_data = await response.json()
                    return user_data.get('username')
                else:
                    return None

    # function to send message
    def send_message(self):
        message = self.message_input.text()
        selected_tab_index = self.tabs.currentIndex()
        
        if selected_tab_index == 0:
            selected_item = self.friends_list.currentItem()
            if selected_item:
                recipient_id = selected_item.data(Qt.UserRole)
                asyncio.run(self.send_direct_message(recipient_id, message))
        elif selected_tab_index == 1:
            selected_item = self.servers_list.currentItem()
            if selected_item:
                channel_id = selected_item.data(Qt.UserRole)
                asyncio.run(self.send_channel_message(channel_id, message))

    # function to send direct message
    async def send_direct_message(self, recipient_id, message):
        headers = {"authorization": f"{self.token}", "content-type": "application/json"}
        payload = {"content": message}
        async with aiohttp.ClientSession() as session:
            async with session.post(f"https://discord.com/api/v9/channels/{recipient_id}/messages", headers=headers, json=payload) as response:
                if response.status != 200:
                    QMessageBox.warning(self, "Error", "Failed to send message.")

    # function to send channel message
    async def send_channel_message(self, channel_id, message):
        headers = {"authorization": f"{self.token}", "content-type": "application/json"}
        payload = {"content": message}
        async with aiohttp.ClientSession() as session:
            async with session.post(f"https://discord.com/api/v9/channels/{channel_id}/messages", headers=headers, json=payload) as response:
                if response.status != 200:
                    QMessageBox.warning(self, "Error", "Failed to send message.")

    # function to refresh messages
    def refresh_messages(self):
        selected_tab_index = self.tabs.currentIndex()
        if selected_tab_index == 0:
            selected_item = self.friends_list.currentItem()
            if selected_item:
                asyncio.run(self.load_direct_messages(selected_item))
        elif selected_tab_index == 1:
            selected_item = self.servers_list.currentItem()
            if selected_item:
                asyncio.run(self.handle_channel_click(selected_item))

if __name__ == "__main__":
    app = QApplication(sys.argv)
    client = Naticord()
    client.show()
    sys.exit(app.exec_())

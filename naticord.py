import sys
import asyncio
import aiohttp
import configparser
import os
import time
import hashlib
import xml.etree.ElementTree as ET
from PyQt5.QtWidgets import QApplication, QWidget, QVBoxLayout, QListWidget, QTextEdit, QLineEdit, QPushButton, QDialog, QListWidgetItem, QMessageBox, QGridLayout, QLabel, QComboBox
from PyQt5.QtCore import Qt, QTimer, QSize
from PyQt5.QtGui import QPixmap, QIcon, QFont, QFontDatabase, QPainter

class LoginScreen(QDialog):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Login to Naticord")
        self.layout = QVBoxLayout()

        self.token_input = QLineEdit()
        self.token_input.setPlaceholderText("Enter your token")
        self.token_input.setStyleSheet("background-color: #2C2F33; color: #FFFFFF; border: 1px solid #7289DA; border-radius: 5px;")
        self.layout.addWidget(self.token_input)

        self.language_combo = QComboBox()
        self.language_combo.addItems(["English", "Polish", "Italian", "Estonian", "Swedish", "Romanian", "Bulgarian", "Persian", "French", "Spanish", "Seal", "Silly", "Romanian (Gangster)","Roadman", "Brainrot"])
        self.layout.addWidget(self.language_combo)

        self.login_button = QPushButton("Login")
        self.login_button.clicked.connect(self.login)
        self.login_button.setStyleSheet("background-color: #7289DA; color: #FFFFFF; border: 1px solid #7289DA; border-radius: 5px;")
        self.layout.addWidget(self.login_button)

        self.setLayout(self.layout)

    def login(self):
        token = self.token_input.text().strip()
        language = self.language_combo.currentText()
        if token:
            config = configparser.ConfigParser()
            config['auth'] = {'token': token, 'language': language}
            config_path = os.path.join(os.path.expanduser("~"), "config.ini")
            with open(config_path, 'w') as configfile:
                config.write(configfile)
            self.accept()
        else:
            QMessageBox.critical(self, get_translation("Error"), get_translation("InvalidToken"))

class Naticord(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Naticord")
        self.resize(1280, 720)  

        self.layout = QGridLayout()
        self.setLayout(self.layout)

        self.setStyleSheet("background-color: #36393F;")

        self.servers_list = QListWidget()
        self.servers_list.setIconSize(QSize(50, 50))
        self.servers_list.setVerticalScrollBarPolicy(Qt.ScrollBarAlwaysOff)
        self.servers_list.setHorizontalScrollBarPolicy(Qt.ScrollBarAlwaysOff)   
        self.servers_list.setStyleSheet("background-color: #36393F; color: #FFFFFF; border: none;")
        self.servers_list.setFixedWidth(55)  
        self.layout.addWidget(self.servers_list, 0, 0, 2, 1)

        self.friends_list = QListWidget()
        self.friends_list.setIconSize(QSize(25, 25))
        self.friends_list.setVerticalScrollBarPolicy(Qt.ScrollBarAlwaysOff)
        self.friends_list.setHorizontalScrollBarPolicy(Qt.ScrollBarAlwaysOff)
        self.friends_list.setStyleSheet("background-color: #36393F; color: #FFFFFF; border: none; font-size: 14px;")
        self.friends_list.setFixedWidth(150)
        self.layout.addWidget(self.friends_list, 0, 1, 2, 1)

        self.right_panel = QWidget()
        self.right_panel_layout = QVBoxLayout()
        self.right_panel.setLayout(self.right_panel_layout)

        self.messages_label = QLabel(get_translation("Message"))
        self.messages_label.setStyleSheet("color: #FFFFFF; font-weight: bold;")
        self.right_panel_layout.addWidget(self.messages_label)

        self.messages_text_edit = QTextEdit()
        self.messages_text_edit.setReadOnly(True)
        self.messages_text_edit.setStyleSheet("background-color: #36393F; color: #FFFFFF; border: 1px solid #7289DA; border-radius: 5px;")
        self.right_panel_layout.addWidget(self.messages_text_edit)

        self.message_input = QLineEdit()
        self.message_input.returnPressed.connect(self.send_message)
        self.message_input.setStyleSheet("background-color: #2C2F33; color: #FFFFFF; border: 1px solid #7289DA; border-radius: 5px;")
        self.right_panel_layout.addWidget(self.message_input)

        self.layout.addWidget(self.right_panel, 0, 2, 1, 1)

        self.token, self.language = self.get_token_and_language()
        if not self.token:
            self.show_login_screen()
        else:
            self.load_data()

        self.refresh_timer = QTimer(self)
        self.refresh_timer.timeout.connect(self.refresh_messages)
        self.refresh_timer.start(3000)

    def show_login_screen(self):
        login_screen = LoginScreen()
        if login_screen.exec_() == QDialog.Accepted:
            self.token, self.language = self.get_token_and_language()
            self.load_data()
        else:
            sys.exit()

    def get_token_and_language(self):
        config = configparser.ConfigParser()
        config_path = os.path.join(os.path.expanduser("~"), "config.ini")
        if os.path.exists(config_path):
            config.read(config_path)
            if 'auth' in config and 'token' in config['auth'] and 'language' in config['auth']:
                return config['auth']['token'], config['auth']['language']
        return None, None

    def load_data(self):
        asyncio.run(self.load_friends())
        asyncio.run(self.load_servers())

    async def load_friends(self):
        headers = {"authorization": f"{self.token}"}
        async with aiohttp.ClientSession() as session:
            async with session.get("https://discord.com/api/v9/users/@me/channels", headers=headers) as response:
                if response.status == 200:
                    channels_data = await response.json()
                    existing_friends = set()
                    for channel in channels_data:
                        recipients = channel.get("recipients", [])
                        if recipients:
                            friend_id = recipients[0].get("id")
                            if friend_id not in existing_friends:
                                friend_info = await self.get_friend_info(friend_id)
                                item = QListWidgetItem()
                                if friend_info['avatar_url']:
                                    pixmap = await self.load_image(friend_info['avatar_url'])
                                    item.setIcon(QIcon(pixmap))
                                item.setText(friend_info['username'])  
                                item.setData(Qt.UserRole, channel['id'])  
                                self.friends_list.addItem(item)
                                existing_friends.add(friend_id)
                    self.friends_list.itemDoubleClicked.connect(self.load_direct_messages)
                else:
                    QMessageBox.warning(self, get_translation("Error"), get_translation("ErrorFetchFriends"))

    async def load_servers(self):
        headers = {"authorization": f"{self.token}"}
        async with aiohttp.ClientSession() as session:
            async with session.get("https://discord.com/api/v9/users/@me/guilds", headers=headers) as response:
                if response.status == 200:
                    servers_data = await response.json()
                    for server in servers_data:
                        server_icon = server.get("icon")
                        item = QListWidgetItem()
                        if server_icon:
                            icon_url = f"https://cdn.discordapp.com/icons/{server['id']}/{server_icon}.png"
                            pixmap = await self.load_image(icon_url)
                            item.setIcon(QIcon(pixmap))
                        else:
                            server_name = server.get("name", "Unknown Server")
                            pixmap = self.create_server_icon(server_name)
                            item.setIcon(QIcon(pixmap))
                        item.setData(Qt.UserRole, server['id'])  
                        self.servers_list.addItem(item)
                    self.servers_list.itemDoubleClicked.connect(self.load_server_channels)  
                else:
                    QMessageBox.warning(self, get_translation("Error"), get_translation("ErrorFetchServers"))

    async def load_direct_messages(self, item):
        self.messages_text_edit.clear()
        channel_id = item.data(Qt.UserRole)
        if channel_id:
            headers = {"authorization": f"{self.token}"}
            async with aiohttp.ClientSession() as session:
                async with session.get(f"https://discord.com/api/v9/channels/{channel_id}/messages", headers=headers, params={"limit": 20}) as response:
                    if response.status == 200:
                        messages_data = await response.json()
                        self.display_messages(messages_data)
                    else:
                        QMessageBox.warning(self, get_translation("Error"), get_translation("ErrorFetchMessages"))

    async def load_server_channels(self, item):
        self.messages_text_edit.clear()
        server_id = item.data(Qt.UserRole)  
        if server_id:
            headers = {"authorization": f"{self.token}"}
            async with aiohttp.ClientSession() as session:
                async with session.get(f"https://discord.com/api/v9/guilds/{server_id}/channels", headers=headers) as response:
                    if response.status == 200:
                        channels_data = await response.json()
                        self.friends_list.clear()
                        for channel in channels_data:
                            channel_name = channel.get("name", "Unnamed Channel")
                            channel_id = channel.get("id")
                            channel_item = QListWidgetItem(channel_name)
                            channel_item.setData(Qt.UserRole, channel_id)
                            self.friends_list.addItem(channel_item)
                    else:
                        QMessageBox.warning(self, get_translation("Error"), get_translation("ErrorFetchServerChannels"))

    def display_messages(self, messages):
        self.messages_text_edit.clear()
        messages_html = ""
        for message in reversed(messages):
            author = message.get("author", {}).get("username", "unknown")
            content = self.format_messages(message.get("content", ""))
            messages_html += f"<b>{author}:</b> {content}<br>"
        self.messages_text_edit.setHtml(messages_html)


    def format_messages(self, content):
        content = content.replace("`", "```")

        if content.startswith("# "):
            content = f"<h1>{content[2:]}</h1>"
        elif content.startswith("## "):
            content = f"<h2>{content[3:]}</h2>"
        elif content.startswith("### "):
            content = f"<h3>{content[4:]}</h3>"

        if content.startswith("- "):
            content = content.replace("- ", "&#8226; ")

        content = content.replace("***", "<b><i>", 1)
        if "***" in content:
            content = content.replace("***", "</i></b>", 1)

        content = content.replace("**", "<b>", 1)
        if "**" in content:
            content = content.replace("**", "</b>", 1)

        content = content.replace("||", "<spoiler>", 1)
        if "||" in content:
            content = content.replace("||", "</spoiler>", 1)

        content = content.replace("_", "<u>", 1)
        if "_" in content:
            content = content.replace("_", "</u>", 1)

        if content.startswith("> "):
            content = f"<blockquote>{content[2:]}</blockquote>"
        elif content.startswith(">>> "):
            lines = content.split("\n")
            blockquote_lines = [f"| {line[4:]}" if line.startswith(">>> ") else line for line in lines]
            content = "<br>".join(blockquote_lines)

        return content

    async def get_friend_info(self, user_id):
        headers = {"authorization": f"{self.token}"}
        retry = 0
        while retry < 5:
            async with aiohttp.ClientSession() as session:
                async with session.get(f"https://discord.com/api/v9/users/{user_id}", headers=headers) as response:
                    if response.status == 200:
                        user_data = await response.json()
                        return {'username': user_data.get('username'), 'avatar_url': f"https://cdn.discordapp.com/avatars/{user_id}/{user_data.get('avatar')}.png"}
                    else:
                        retry += 1
        return {'username': 'Unknown', 'avatar_url': ''}

    async def load_image(self, url):
        cache_folder = os.path.join(os.path.expanduser("~"), ".naticord_cache")
        os.makedirs(cache_folder, exist_ok=True)
        filename = hashlib.md5(url.encode()).hexdigest()
        file_path = os.path.join(cache_folder, filename)
        if not os.path.exists(file_path):
            async with aiohttp.ClientSession() as session:
                async with session.get(url) as resp:
                    data = await resp.read()
                    with open(file_path, 'wb') as f:
                        f.write(data)
        pixmap = QPixmap(file_path)
        return pixmap

    def send_message(self):
        message = self.message_input.text()
        selected_item = self.friends_list.currentItem()
        if selected_item:
            recipient_id = selected_item.data(Qt.UserRole)
            asyncio.run(self.send_direct_message(recipient_id, message))

    async def send_direct_message(self, recipient_id, message):
        headers = {"authorization": f"{self.token}", "content-type": "application/json"}
        payload = {"content": message}
        async with aiohttp.ClientSession() as session:
            async with session.post(f"https://discord.com/api/v9/channels/{recipient_id}/messages", headers=headers, json=payload) as response:
                if response.status != 200:
                    QMessageBox.warning(self, get_translation("Error"), get_translation("ErrorSendMessage"))

    def refresh_messages(self):
        selected_item = self.friends_list.currentItem()
        if selected_item:
            asyncio.run(self.load_direct_messages(selected_item))

    def create_server_icon(self, server_name):
        words = server_name.split()
        initials = [word[0].upper() for word in words]
        icon_text = ''.join(initials)
        pixmap = QPixmap(50, 50)
        pixmap.fill(Qt.transparent)
        painter = QPainter(pixmap)
        painter.setPen(Qt.white)
        painter.drawText(pixmap.rect(), Qt.AlignCenter, icon_text)
        painter.end()
        return pixmap

def get_translation(tag):
    config = configparser.ConfigParser()
    config_path = os.path.join(os.path.expanduser("~"), "config.ini")
    if os.path.exists(config_path):
        config.read(config_path)
        if 'auth' in config and 'language' in config['auth']:
            language = config['auth']['language'].lower()
            language_file = f"languages/language_{language}.xml"
            if os.path.exists(language_file):
                tree = ET.parse(language_file)
                root = tree.getroot()
                for message in root:
                    if message.tag == tag:
                        return message.text
    return tag

if __name__ == "__main__":
    app = QApplication(sys.argv)

    font_database = QFontDatabase()
    font_id = font_database.addApplicationFont("assets/noto.ttf")
    font_family = QFontDatabase.applicationFontFamilies(font_id)[0]

    font = QFont(font_family)
    app.setFont(font)

    client = Naticord()
    client.show()
    sys.exit(app.exec_())

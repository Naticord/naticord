# naticord
![work](https://img.shields.io/badge/work-in%20progress-green?style=plastic)
![please star this <3](https://img.shields.io/badge/please%20star%20this%20%3C3-yellow?style=plastic)

> [!CAUTION]
> ### Do not blame me if you get banned.
> Discord may ban you if they catch you using a custom client, this hasn't happened to anyone yet using naticord or other client mods such as Vencord.

Naticord / Native-cord is a native Discord client made in Python.

Why make a native client or even a custom client at all? Well, it's because Discord's half baked web app is **shit**. I want to have a simple yet good experience while having my RAM not being taken by Discord. So this was the answer.

The official Discord app goes to 400MB idle while this can go all the way low to 25MB!

Also feel free to make changes to the source! Make my code better as mine is pretty shit as it is, so I accept any PRs I get.

The UI is also sort of inspired Windows Live Messenger (long live WLM)

<a href="https://star-history.com/#n1d3v/naticord&Date">
 <picture>
   <source media="(prefers-color-scheme: dark)" srcset="https://api.star-history.com/svg?repos=n1d3v/naticord&type=Date&theme=dark" />
   <source media="(prefers-color-scheme: light)" srcset="https://api.star-history.com/svg?repos=n1d3v/naticord&type=Date" />
   <img alt="Star History Chart" src="https://api.star-history.com/svg?repos=n1d3v/naticord&type=Date" />
 </picture>
</a>

# Installation
There are 3 different ways to install naticord on Windows, Linux and macOS.
### Windows
```
pip install pyqt5 requests aiohttp
```
### Linux
```
pip3 install pyqt5 requests aiohttp
```
### macOS
First install Homebrew (brew.sh). Then in terminal run
```
brew install python
```
Then run
```
brew install pyqt5 && brew install python-requests && pip3 install aiohttp --break-system-packages 
```
And then just run `naticord.py`

Here's a revised version:

---

# How to Log In:

1. Open Discord on any web browser and log in.
2. Refresh the page (Press F5).
3. Access Developer Tools by pressing Ctrl + Shift + I (or right-click and select "Inspect Element").
4. Navigate to Responsive Design Mode. (In Chrome, it's "Toggle Device Toolbar").
   - In Firefox, look for an icon featuring a phone and a tablet. In Chrome, it's a phone next to a laptop.
5. Locate local storage. (In Chrome, navigate to Application and find local storage).
6. In the filter tab, search for "token".

![Find Token](https://github.com/Shavixinio/naticord/assets/54279284/7784ece1-3dbf-4fe5-916b-877c86404be8)

7. Retrieve the token and paste it into the Naticord login page.

![Paste Token](https://github.com/Shavixinio/naticord/assets/54279284/c588eb0d-edf6-4ab7-b2e5-c9ddbb298892)

# Switching Accounts:

1. Navigate to C:/Users/YourName/Config.ini.
2. Replace the current token with the one associated with your other account.

# What's working
- Logging in with a token
- Friends
- DMs
- Sending messages in DMs
- Basic server functionality
- Ping support
# Todo
- ~~Server functionality~~
- ~~Better UI~~
- ~~Fix servers~~
- Add editing, reply and delete
- Image support
- Emojis
- Server channel categories
- Voice calling (might not happen)
# Bugs
- ~~App lags when DMs refresh (not a bug thats a feature™)~~
- Pings are broken (will be fixed pretty shortly)
# Screenshots
![image](https://github.com/n1d3v/naticord/assets/135556230/f16cb368-cade-4fdf-b6a7-e8cfdf2d469a)

---

![naticord2 1](https://github.com/n1d3v/naticord/assets/135556230/db2b6f36-c095-42b8-adff-770b7a83d976)





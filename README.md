<h1>
  <img src="https://github.com/user-attachments/assets/b88cfb0f-5459-4256-aeba-82782c79c2db" alt="Frame 115">
  Naticord
</h1>

![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)
![Language](https://img.shields.io/badge/language-C%23-%23239120.svg)

Naticord is a 3rd-party Discord client made using C# and Windows Forms. This was made with the intention of looking good on all Windows versions from 7 - 11 and reducing the RAM and CPU usage of Discord.

> [!WARNING]
> Currently, the code is being rewritten and some features are unstable or broken. We apologize for the inconvenience. We promise it'll be worth the wait. See the current functionality below.

> **Note:** Discord may break the client at any time, if that happens, please report it here.

> **Motto:** *A native Discord experience.*

<p align="center">
  <a href="https://www.star-history.com/#Naticord/naticord&Date">
   <picture>
     <source media="(prefers-color-scheme: dark)" srcset="https://api.star-history.com/svg?repos=Naticord/naticord&type=Date&theme=dark" />
     <source media="(prefers-color-scheme: light)" srcset="https://api.star-history.com/svg?repos=Naticord/naticord&type=Date" />
     <img alt="Star History Chart" src="https://api.star-history.com/svg?repos=Naticord/naticord&type=Date" />
   </picture>
  </a>
</p>

# Functionality
- Login
  - Email (with 2FA): ✔️
  - Token: ✔️
- Client
  - Loading user information (Username, avatar, etc.): ✔️
  - Loading friends, group chats and servers: Partially implemented (Friends only)
  - Loading messages: ✔️
  - Sending messages (incl. uploading): ✔️ 
  - Websockets: ✔️ 
- Misc
  - Client updating: Not implemented (Planned)
  - Markdown support: ✔️ (A bit buggy, but a work-in-progress)
  - Notification support: ✔️
  - Profile viewing: Not implemented (Planned)
  - Viewing statuses (non-custom): ✔️
  - Pasting files from clipboard: ✔️
  - Image viewing: ✔️ (GIFs aren't supported)
  - Video viewing: Not implemented or planned
  - File viewing: Not implemented (Planned)
- OS support
  - Windows 11 / 10 / 8.1 / 7: ✔️
  - Windows Vista / XP: May work under extended kernels (Untested)
  - Linux: ✔️ (Under WINE)
  - macOS: ✔️ (Under WINE)

You may submit a feature request using the template in **[Issues](https://github.com/Naticord/naticord/issues)**
# Credits
- Discorduserdoccers: Unofficial documentation for Discord
- Discord Messenger: Some design inspiration (Login screen)
- Tech Stuff: Helping out with some stuff (2FA backend)

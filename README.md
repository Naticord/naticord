<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="https://github.com/user-attachments/assets/f688e553-ca56-4c1b-af2d-385766540ad6" />
    <source media="(prefers-color-scheme: light)" srcset="https://github.com/user-attachments/assets/72f612cb-6b3e-4d9e-b9df-a5480d624ba2" />
    <img src="https://github.com/user-attachments/assets/72f612cb-6b3e-4d9e-b9df-a5480d624ba2" alt="Naticord Logo" />
  </picture>
</p>

<p align="center">Naticord is a native Discord client made using C# with Windows Forms.</p>

<p align="center">
 <a href="https://naticord.lol">Website</a> · <a href="https://discord.com/invite/Hr7tC837ZW">Discord Server</a>
</p>

---

# Building Naticord

There are two ways to build Naticord: the **Easy Route** or the **Command-Line Route**.

### The Easy Route
1. Open **Visual Studio 2022**.
2. Launch the solution.
3. Allow Visual Studio to automatically install all the required NuGet packages.
4. From the top menu, select **Build** and click on **Build Solution**.
5. You can find your build at:
   - `{naticordDir}\Naticord\bin\Debug\Naticord.exe` for a Debug build
   - `{naticordDir}\Naticord\bin\Release\Naticord.exe` for a Release build

### The Command-Line Route
*This method requires Visual Studio 2022 to be installed.*

1. Open **CMD** (Command Prompt).
2. Clone the Naticord repository using **git** or **GitHub Desktop**.
3. Change directory into the Naticord folder:
   ```bash
   cd naticord
   ```
4. Run the following command to restore all packages
   ```bash
   nuget restore Naticord\Naticord.sln
   ```
5. Run the command to generate a debug build
   ```bash
   msbuild Naticord/Naticord.sln -t:rebuild -property:Configuration=Debug
   ```
   - For a release build, replace `Debug` with `Release`
5. You can find your build at:
   - `{naticordDir}\Naticord\bin\Debug\Naticord.exe` for a Debug build
   - `{naticordDir}\Naticord\bin\Release\Naticord.exe` for a Release build

# What's working
- Logging in via email and password ~~(incl, 2FA)~~ (2FA is broken for now)
- Friends / Groups / Servers
- Searching functionality
- Blocking people, leaving groups and unfriending people
- File uploading (Does not bypass Discord's limit)
- Typing indicators
- Image viewing
- Messaging
- Embeds
- Plugins
- Pings
# Planned functionality
- Voice chat
  - This may be implemented very soon, thanks to Aerovoice!
- Statuses
  - Still a work in progress, definitely soon though. 
- Edit reply and delete.
  - Will be very soon! Once DMs, groups and servers get a revamp.
# Bugs
- When starting the WebSocket / the WebSocket starts to reconnect it will cause a memory spike and go up to 280MB and go back down to 80MB, the reason for this is unknown and will most likely not be fixed.
# Credits
- [jukfiuune](https://github.com/jukfiuune): Original source of Naticord (Aerocord) and alot of help!

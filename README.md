# naticord
![skillicns](https://skillicons.dev/icons?i=discord,cs,dotnet)

Naticord / Native-cord is a native Discord client made in C#.

Supports Windows Vista - Windows 11 (Windows Vista needs a proxy to work.)

<a href="https://star-history.com/#n1d3v/naticord&Date">
 <picture>
   <source media="(prefers-color-scheme: dark)" srcset="https://api.star-history.com/svg?repos=Naticord/naticord&type=Date&theme=dark" />
   <source media="(prefers-color-scheme: light)" srcset="https://api.star-history.com/svg?repos=Naticord/naticord&type=Date" />
   <img alt="Star History Chart" src="https://api.star-history.com/svg?repos=Naticord/naticord&type=Date" />
 </picture>
</a>

# Changelog
Changelog can be found in the root directory of this repo as `CHANGELOG.md`.

# Contributing
Contributing guide can be found in the root directory of this repo as `CONTRIBUTORS.md`.

# Installation
Download naticord-setup.exe if you are on Windows Vista+ If you are on Vista, you will need to setup a proxy for communicating with the Discord API (this will soon not be needed). If you are on Linux, your best bet is using Wine.

### Updating
To update, you can just get the latest version and install it.

# Building Naticord
There are 2 ways to build Naticord, the easy route or the command-line route.

### The Easy Route
- Open Visual Studio 2022
- Launch the solution
- Install all the NuGet packages (it will do it automatically)
- Select Build at the top and click Build Solution.
- You can find your build at {naticordDir}\Naticord\bin\Debug\Naticord.exe or {naticordDir}\Naticord\bin\Release\Naticord.exe depending on what build type you have made.
### The command-line route
This requires VS 2022 installed.

- Open CMD
- Clone the naticord repository (this one)
- CD into the naticord directory
- Run `nuget restore Naticord\Naticord.sln` this will restore all the packages Naticord uses.
- Run `msbuild Naticord/Naticord.sln -t:rebuild -property:Configuration=Debug` this will generate a debug release of Naticord. If you want a release build switch out Debug for Release.
- You can find your build at {naticordDir}\Naticord\bin\Debug\Naticord.exe or {naticordDir}\Naticord\bin\Release\Naticord.exe depending on what build type you have made.

# What's working
- Logging in with an email and password (with MFA)
- Friends
- Groups
- Sending messages
- Full server functionality
- Server channel categories
- File uploading
- Pings
- Hyperlinks
- Typing indicators
- Display names and friend nicknames
- Websockets
- Image viewing
- Embeds
- Plugins (experimental)
- Searching for either friends or servers
- Copying and pasting images from the clipboard
- Block, unfriend and leave groups / friends.
# Planned functions
- Voice calling (do not expect this, ever)
- Statuses
- Hide hidden channels in servers
- Other various channels such as fourms and such
- Edit reply and delete.
# Bugs
- Sometimes markdown glitches out.
# naticord Timeline
![image](https://github.com/user-attachments/assets/14a4c793-a583-4ee6-bcb8-366d1a9b31c2)


# Naticord
Naticord is a Discord client focused on bringing the best experience possible natively.

Naticord uses DirectUI and Windows Forms, both bringing a native look to the client. DirectUI is an undocumented language made by Microsoft and was never released publicly. The library used in Naticord for DirectUI is [DirectUI.Net](https://github.com/n1d3v/DirectUI.Net).


<p align="center">
  <a href="https://www.star-history.com/#Naticord/naticord&Date">
   <picture>
     <source media="(prefers-color-scheme: dark)" srcset="https://api.star-history.com/svg?repos=Naticord/naticord&type=Date&theme=dark" />
     <source media="(prefers-color-scheme: light)" srcset="https://api.star-history.com/svg?repos=Naticord/naticord&type=Date" />
     <img alt="Star History Chart" src="https://api.star-history.com/svg?repos=Naticord/naticord&type=Date" />
   </picture>
  </a>
</p>

# What is finished?
- Authentication
  - Signing in using a token: ✔️
  - Signing in using a QR code: ✔️
  - Signing in using e-mail and password: ❌ ([See why...]())
- Client
  - User interface: ➖ (Work-in-progress)
  - Discord functionality: ➖ (Work-in-progress)
- Miscellaneous
  - Work-in-progress, list will come in the future!
- OS support (Windows-only)
  - Windows 11 / 10: ✔️
  - Windows 8.1 / 7: ➖ (Not tested!)
  - Windows Vista / XP: ❌ (Implementation soon!)
# Why can't you login in using an e-mail and password?
Discord uses intense security measures to prevent custom clients from signing in using an e-mail and password, meaning there is no reliable way to sign in with this method without getting banned on a custom client. This is why Naticord is moving away from this approach and using easier methods instead like using a QR code to sign in.

This feature will not be coming in the future as it is a pain to maintain and I am not willing to fight through the pain of just getting to sign in.
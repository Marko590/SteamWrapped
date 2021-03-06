# Steam Wrapped

ASP.NET Web API which fetches a users' list of games on Steam, along with hours played and other info,
which will be used alongside a React frontend.

All calls to the Steam Web API are made using [SteamWebAPI2](https://github.com/babelshift/SteamWebAPI2)

## Usage

`https://localhost:{port}/SteamStats/{id}` - returns a JSON object consisting of basic info on a user, and also their games 
with their hours played.
____
`https://localhost:{port}/SteamStats/{id}/achievements/{appId}` - returns a JSON object consisting of info about the game on the Steam page `https://steamcommunity.com/app/{appId}`,
along with the achievements of the user whose steamID is `id`



## To-do
  
- [ ] Add support for custom steam URL (if possible)
- [x] Add error handling for invalid SteamIDs
- [x] Add support for fetching achievement info
- [ ] Add friends list support

#### Steam Wrapped

ASP.NET Web API which fetches a users' list of games, along with hours played and other info,
which will be used alongside a React frontend.

---

### Usage

`https://localhost:{port}/SteamStats/{id}` - returns a JSON object consisting of basic info on a user, and also their games 
with their hours played.



---

## To-do
  
  1. Add support for custom steam URL (if possible)
  2. Add error handling for invalid steamIDs
  3. Add friends list support
  4. Add support for fetching achievement info

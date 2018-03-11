# GroupMeTools

## What is this?
* A small project to download all the images/ videos from a GroupMe group
* Could be developed into a full-fledged API

## How do I download all attatchments from a group?
1. create a developer account on GroupMe's developer page (https://dev.groupme.com/)
  * You can login with your GroupMe account
2. Create an access token
  * You may have to create an application first? (https://dev.groupme.com/applications)
3. Compile this solution/ project with Visual Studio (or whatever you prefer)
4. Run the compiled .exe
5. Follow the instructions on the .exe
  * To get the id of all the groups you are in, go here: https://api.groupme.com/v3/groups?token=$YOUR_ACCESS_TOKEN
  * where $YOUR_ACCESS_TOKEN is your access token from 2.
6. Get your attachments from the /attachments directory

## TODO
* Use REST instead of URLs
  * https://dev.groupme.com/docs/v3
* Filling out the POCO in the "src/json" folder with all the properties

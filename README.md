# Google Drive Uploader - Windows Form App C# .NET
A simple Windows Form application for uploading file into Google Drive.

![UI Preview](https://cloud.githubusercontent.com/assets/6031269/9305702/5c45a07e-44eb-11e5-880f-86fe0073d483.JPG)

## Before starting
Before runnging the application you must create a new application in your Google Deveelopers Console:
   1. Create a new application
   2. Go to "APIs & Auth > Credentials > Create Client ID"
   3. Select "Installed Appliation" when requested
   4. Fill the form with a project's name
   5. Download the JSON file
   
## Authentication
Change the content of the 'client_secret'json' file included in the project with the content of the JSON file you downloaded.
This step is necessary in order to make the app get the credentials from your account.

## Importing the project
When importing the project on VS the refereces must be restored; building and starting the application should automatically solve the problem.

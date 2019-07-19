# automatic-pancake
izzatshehadeh/cautious-broccoli-core


Create appsettings.json

{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "tokenManagement": {
    "secret": "",
    "issuer": "",
    "audience": "",
    "accessExpiration": 1444,
    "refreshExpiration": 2444,
    "firebaseProject": ""
  },
  "ConnectionString": {
    "AppDB": "server=;database=;User ID=;password=;"
  },
  "passwordComplexity": {
    "hasNumber": true,
    "hasUpperChar": false, 
    "hasLowerChar": false,
    "hasSymbols": false,
    "minCharacters": 2,
    "maxCharacters": 30
  },
  "AllowedHosts": "*"
}

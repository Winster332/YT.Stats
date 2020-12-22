# YT.Stats
Lets you track your performance on [youtrack](https://www.jetbrains.com/ru-ru/youtrack/).
The utility connects to your account and enters many indicators of their output to the terminal.

## How use?

You only need to configure the appsettings.config file, and then enter the tasks into the youtrack.

```javascript
{
	"serverUrl": "url", // address to your youtrack. Ex: https://youtrack.yourcompany.me/youtrack
	"token": "your_token", // token of your youtrack. Ex: perm:c5RhbalzbGF2Lm1hcnR5bm92.NTYzMB==.0xuMPIaar5KCdRV2BiiHg7PS814Yas
	"fileName": "report.csv", // output report title
	"fixed-salary": 1000, // your monthly rate
	
	"_comment": "all-csv, last-csv, last-list, render",
	"mode": "render"
}
```

## Screenshots
![N|Solid](https://github.com/Winster332/YT.Stats/blob/master/YT.Stats/Screenshots/photo_2020-03-27_01-13-16.jpg)
![N|Solid](https://github.com/Winster332/YT.Stats/blob/master/YT.Stats/Screenshots/photo_2020-03-27_01-13-17.jpg)


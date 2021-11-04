# overwatch_shit.zip
This is (eventually) going to contain all the software I wrote during my five-year ~imprisonment~ stay playing Overwatch. I'll be adding things as I go through my old drives.

## Notes

1. If it's written in Python, it'll be ugly. I only use the language to quickly write things I need. If you use it for anything else, you're wrong and I don't care.
2. If it's written in D, to compile it you'll need `dmd` or `ldc2`. Both of which can be obtained from https://dlang.org/
3. If it's written in .NET, it's likely something to do with video processing. Make sure you grab whatever deps are required from `NuGet`.
4. I will mark the projects that are actually good examples of project structure/program flow. If a project is unmarked, don't treat it as something you can learn from.


I wrote this code for my use. Not for your use. Most of this was written while I was still playing Overwatch for 12 hours every day, meaning I wanted to get it out of the way so I could go back to solo queue. There are a few projects that this repo will contain that I am proud of, however. None of which are Python based.

<hr>

# Projects not included in this repo
### [overwatch-ocr](https://github.com/zkxjzmswkwl/overwatch-ocr)
Images screen and outputs dictoinaries for each team, containing key/value pairs of what hero each player is on.

Eventually this turned into a contracted project for a team, thus public pushes stopped.

### [Circus Match Server](https://github.com/zkxjzmswkwl/CircusServer)
Fully functional "rank s" equivalent for Overwatch. This must be accompanied by the project below.

### [Circus-Backend](https://github.com/Rankedcircus/Circus-Web-Backend)
Django backend that is consumed by both the Circus frontend, and the Circus matchserver.

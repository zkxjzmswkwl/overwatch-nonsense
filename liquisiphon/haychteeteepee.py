# h = haych innit
import requests
from bs4 import BeautifulSoup as bs
from helpers import get_base

# Liquipedia main_pages differ in html structure from game to game.
# Fuck you, Liquipedia. Stop harcoding 50% of ur shit.
# Because of that, this is exceptionally ugly.
# It's Python, not much to do about it. 
# I'm using it over a proper language because it's fast to write, 
# not because I give a singular fuck about code cleanliness.

def get_player_transfers(game="overwatch"):
    req_url = get_base(game)
    r = requests.get(req_url)

    html = bs(r.text, "html.parser")

    transfers = html.find("div", {"class": "mainpage-transfer"})
    to_team = transfers.find_all("div", {"class": "mainpage-transfer-to-team"})
    from_team = transfers.find_all("div", {"class": "mainpage-transfer-from-team"})
    neutral = transfers.find_all("div", {"class": "mainpage-transfer-neutral"})

    transfers = {"to": [], "neutral": [], "from": []}

    for t in to_team:
        txt = t.find_all("a")[2].text
        old_team = t.find("div", {"class": "OldTeam"}).text
        new_team = t.find("div", {"class": "NewTeam"})
        new_team = new_team.find_all("span")

        if len(new_team) > 1:
            new_team = new_team[0].get("data-highlightingclass")
        
        if len(txt) > 1:
            transfers["to"].append({"name": txt, "old": old_team, "new": new_team})

    for t in from_team:
        txt = t.find("div", {"class": "divCell Name"})
        txt = txt.find_all("a")
        old_team = t.find("div", {"class": "OldTeam"})
        old_team = old_team.find_all("span")
        new_team = t.find("div", {"class": "NewTeam"}).text

        if len(txt[1].text) == 0:
            txt = txt[2].text
        else:
            txt = txt[1].text

        if len(old_team) > 1:
            old_team = old_team[0].get("data-highlightingclass")

        transfers["from"].append({"name": txt, "old": old_team, "new": new_team})

    for t in neutral:
        txt = t.find_all("a")[2].text
        old_team = t.find("div", {"class": "OldTeam"})
        old_team = old_team.find_all("span")
        new_team = t.find("div", {"class": "NewTeam"})
        new_team = new_team.find_all("span")

        if len(new_team) > 1:
            new_team = new_team[0].get("data-highlightingclass")

        if len(old_team) > 1:
            old_team = old_team[0].get("data-highlightingclass")

        if len(txt) > 0:
            transfers["neutral"].append({"name": txt, "old": old_team, "new": new_team})

    return transfers


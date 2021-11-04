from haychteeteepee import get_player_transfers

#b = get_player_transfers("leagueoflegends")
#b = get_player_transfers("valorant")
# This is basically only useful for "overwatch".
# Reason being is Liquipedia hardcodes their html structure, so it varies from game to game.
# Not my fault they're doing stupid shit. But I am choosing to not support anything else
# properly, aside from Overwatch.

b = get_player_transfers("overwatch")

for a in b["to"]:
    print(a)

for a in b["from"]:
    print(a)

for a in b["neutral"]:
    print(a)


# There was a disc.py file in this project that piped all these into a channel whenever
# a player was dropped, traded, or retired. Dunno where that is. 
# If I find it I'll push it here.

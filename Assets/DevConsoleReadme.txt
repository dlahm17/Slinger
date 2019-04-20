Steps to Use Dev Console
1: Press '`' button to bring up dev console
2: write command, punctuate between commands with a period, and punctuate all commands with a period (ex: addgold.10. will give 10 gold to the player)

List of commands
addgold.(integer). --adds gold to character equal to integer
subgold.(integer). --subtracts gold from character equal to integer
addexp.(integer). --adds exp to character equal to integer
subexp.(integer). --subtracts exp from character equal to integer
addwpn.(string). --adds weapon to inventory (note that weapon name must be the same as the file name in the project of the weapon you want to add)
addabi.(string). --adds ability to available abilities without need to collect in world (note that ability name must be the exact same as ability file name in the project)
spawn.
additem.(string).(string).
clearinventory. --clears inventory of all items
stat.(string).(integer).


example commands

addgold.10. (this will add 10 gold to player)
subgold.10. (this will take 10 gold from player)
addexp.10. (this will add 10 exp to player)
subexp.10. (this will take 10 exp from player)
addwpn.BlockRifle. (This will add the block rifle to your inventory)
addabi.Icicle. (this will add the Icicle ability to your available ability list)
spawn is not implemented yet, don't try it
clearinventory. (clears entire inventory, but not the items on your body)
stat.health.10. (increases health stat by 10)
stat.armor.10.
stat.damage.10.
stat.magicdamage.10.
stat.magicarmor.10.
stat.speed.10.
stat.stealth.10.

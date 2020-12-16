# Project Title

Case - Trading Card

## Prerequirements

* Visual Studio 2019
* .NET Core v3.1

## How To Run

* Open solution in Visual Studio 2019
* Set TradingCardGame project as Startup Project and build the project.
* Run the application.

## User Guide

* First enter your username. If you have a user, you can login. If not, it will be created automatically.
* You will encounter Json registered users and artificial intelligence. You can play against it by choosing one of them.
* Played sequentially if you choose the user. If you choose artificial intelligence, it will automatically make its moves.

## Getting Started

**REQUIREMENTS**
* Follow OO Solid Principles and write easy to understand code.
* Be Ready to add new requirements, do pair programming and explain your code.
* Do not copy the code from google. Provide your own implementation.

**PROBLEM DESCRIPTION**
In this Kata you will be implementing a rudimentary two-player trading card game. The rules are loosely based on Blizzard Hearthstone (http://us.battle.net/hearthstone/en/ ) which itself is an already much simpler and straight-forward game compared to other TCGs like Magic: The Gathering (http://www.wizards.com/magic/ ).

**PREPARATION**
* Each player starts the game with 30 Health and 0 Mana slots
* Each player starts with a deck of 40 Damage cards with the following Mana
costs: 0,0,0,0,1,1,1,1,2,2,2,2,2,2,3,3,3,3,3,3,3,3,4,4,4,4,4,4,5,5,5,5,6,6,6,6,7,7,8,8
* From the deck each player receives 3 random cards has his initial hand

**GAMEPLAY**
* The active player receives 1 Mana slot up to a maximum of 10 total slots
* The active player’s empty Mana slots are refilled
* The active player draws a random card from his deck
* The active player can play as many cards as he can afford. Any played card empties Mana slots and deals immediate damage to the opponent player equal to its Mana cost.
* If the opponent player’s Health drops to or below zero the active player wins the game
* If the active player can’t (by either having no cards left in his hand or lacking sufficient Mana to pay for any hand card) or simply doesn’t want to play another card, the opponent player becomes active

**SPECIAL RULES**
* Overload: If a player draws a card that lets his hand size become >5 that card is discarded instead of being put into his hand.
* Dud Card: The 0 Mana cards can be played for free but don’t do any damage either. They are just annoyingly taking up space in your hand.

## Versioning

The versions available, see the [Trading Card Game](https://github.com/meminizmirli/TradingCardGame). 

## Authors

* **Mehmet Emin İzmirli** - *Initial work* - [meminizmirli](https://github.com/meminizmirli)



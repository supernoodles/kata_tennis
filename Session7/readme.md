# tennis-kata

Rules:

http://www.tennistips.org/tennis-scoring.html

* A tennis player must :
	- earn a minimum of 4 points to win a game.
	- play at least 6 games to win a set.
	- win no less than 2 sets (at times 3 sets) to close out a match.

* For simplicity, the first player score is always announced first: 

	- Points Earned 0 = 0 Games Points or 'Love'
	- Points Earned 1 = 15 Game Points
	- Points Earned 2 = 30 Game Points
	- Points Earned 3 = 40 Game Points
	- Points Earned 4 = Game Over (2 Point Advantage Required)
	
The winner of a tennis game must win with a two point advantage. In other words,
 if the score is 40-0 and the player 1 wins the next point, the player 1 wins the game.

Deuce is the terminology expressed when the score in the game is 40-40. To win 
the game when the score is deuce, a competitor must score two consecutive points 
(one immediately after the other) --- otherwise the score reverts back to deuce.
The first competitor to score after the score is deuce is ahead by one point and 
now has what is termed in tennis as having the Advantage or 'Ad' for short.

*Inputing the score:
	In this exercise the input of the score will be done via the console. 
	The program will prompt the user and ask for the name of the player who won the points.

Scenario: Start of match

Given 
* a 3-set match,
* played between Player 1 and Player 2,
* at the start of the game

When
* the game is about to start

Then
* the scoreboard should show:

```	
Player   | 1 | 2 | 3 | Game
---------------------------
Player 1 | 0 |   |   | 0
Player 2 | 0 |   |   | 0
```

_____________________________________________________

Scenario: First point

Given 
* a 3-set game,
* played between Player 1 and Player 2,
* in the first game of the match

When
* Player 1 scores a point

Then
* the scoreboard should show:
	
```
Player   | 1 | 2 | 3 | Game
---------------------------
Player 1 | 0 |   |   | 15
Player 2 | 0 |   |   | 0
```


Given
* a 3-set game,
* played between Player 1 and Player 2,
* where the first 2 sets were scored 7-5 and 6-4,
* and the 3rd set is currently 2-0,
* and the current game is currently 40-0

When
* Player 2 scores a point

Then
* the scoreboard should show:

```
Player   | 1 | 2 | 3 | Game
---------------------------
Player 1 | 7 | 4 | 2 | 40
Player 2 | 5 | 6 | 0 | 15
```

__________________________________________________

Scenario: Deuce

Given 
* a 3-set game,
* played between Player 1 and Player 2,
* in the first game of the match,
* where the current score is deuce

When
* Player 1 scores a point

Then
* the scoreboard should show:

```	
Player   | 1 | 2 | 3 | Game
---------------------------
Player 1 | 0 |   |   | A
Player 2 | 0 |   |   | 40
```

___________________________________________________

Scenario: Winning a game

Given 
* a 3-set game,
* played between Player 1 and Player 2,
* in the first game of the match,
* where the current score is 40-30

When
* Player 1 scores a point

Then
* the scoreboard should show:

```	
Player   | 1 | 2 | 3 | Game
---------------------------
Player 1 | 1 |   |   | 0
Player 2 | 0 |   |   | 0
```

___________________________________________________

Scenario: Winning a match

Given 
* a 3-set match,
* played between Player 1 and Player 2,
* in the last game of the match,
* where the first set score is 7-5,
* and the second set score is 4-6,
* and the last set score is 5-4,
* and the game score is 40-15
	
When
* Player 1 scores a point
Then
* the scoreboard should show the final score as:

```	
Player   | 1 | 2 | 3 | Game
---------------------------
Player 1*| 7 | 4 | 6 | 
Player 2 | 5 | 6 | 4 | 
```
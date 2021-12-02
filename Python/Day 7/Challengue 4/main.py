def convert_list_to_string(list_to_convert):
    string_to_return = ""

    for letter in list_to_convert:
        string_to_return += letter
    
    return string_to_return

import random

stages = ['''
  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
=========
''', '''
  +---+
  |   |
  O   |
 /|\  |
 /    |
      |
=========
''', '''
  +---+
  |   |
  O   |
 /|\  |
      |
      |
=========
''', '''
  +---+
  |   |
  O   |
 /|   |
      |
      |
=========''', '''
  +---+
  |   |
  O   |
  |   |
      |
      |
=========
''', '''
  +---+
  |   |
  O   |
      |
      |
      |
=========
''', '''
  +---+
  |   |
      |
      |
      |
      |
=========
''']

word_list = ["ardvarkl", "baboon", "camel"]

word_to_guess = random.choice(word_list)
display_word = []
for _ in range(len(word_to_guess)):
    display_word += "_"

print("Welcome to the hangman game!")

lives = len(stages) - 1

game_ended = False
while not game_ended:  
    guess = input(f"Please input a letter to guess the hangman word ({convert_list_to_string(display_word)}): ").lower()

    guess = guess[0] if len(guess) > 0 else ""
    if (guess == ""):
        continue

    if (guess in word_to_guess):
        for index in range(len(word_to_guess)):
            if (word_to_guess[index] == guess):
                display_word[index] = guess
        if convert_list_to_string(display_word) == word_to_guess:
            game_ended = True
    else:
        print("That letter is not in the word")
        lives -= 1
        if (lives == 0):
            game_ended = True
            
    print(stages[lives])

if (lives > 0):
    print("You guessed the word, you win!")
else:
    print("You did not guess the word correctly, you lose!")
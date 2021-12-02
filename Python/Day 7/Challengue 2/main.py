def convert_list_to_string(list_to_convert):
    string_to_return = ""

    for letter in list_to_convert:
        string_to_return += letter
    
    return string_to_return

import random

word_list = ["ardvarkl", "baboon", "camel"]

word_to_guess = random.choice(word_list)
display_word = []
for _ in range(len(word_to_guess)):
    display_word += "_"

print("Welcome to the hangman game!")

guess = input(f"Please input a letter to guess the hangman word ({convert_list_to_string(display_word)}): ").lower()

guess = guess[0] if len(guess) > 0 else ""
if (guess == ""):
    exit

if (guess in word_to_guess):
    for index in range(len(word_to_guess)):
        if (word_to_guess[index] == guess):
            display_word[index] = guess
    print(f"That letter is in the word, the hangman word now looks like: {convert_list_to_string(display_word)}")
else:
    print("That letter is not in the word")
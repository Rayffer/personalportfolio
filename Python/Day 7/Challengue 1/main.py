import random

word_list = ["ardvarkl", "baboon", "camel"]

word_to_guess = random.choice(word_list)

print("Welcome to the hangman game!")
guess = input("Please input a letter to guess the hangman word: ")[0].lower()

for letter in word_to_guess:
    print("Correct guess" if guess == letter else "Incorrect guess")
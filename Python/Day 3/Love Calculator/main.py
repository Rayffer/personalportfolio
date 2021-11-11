print("Welcome to the Love Calculator")
your_name = input("What is your name?").lower()
their_name = input("What is their name?").lower()

letters = ['t', 'r', 'u', 'e', 'l', 'o', 'v', 'e']

score = 0
for letter in letters:
    score += your_name.count(letter) + their_name.count(letter)

suffix = ""
if (score < 10 or score > 90):
    suffix = ", you go together like coke and mentos"
elif (score < 40 and score > 50):
    suffix = ", you are alright together"

print(f"Your score is {score}{suffix}")
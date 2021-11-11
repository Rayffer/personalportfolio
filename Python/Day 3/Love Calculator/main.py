print("Welcome to the Love Calculator")
your_name = input("What is your name?").lower()
their_name = input("What is their name?").lower()

true_letters = ['t', 'r', 'u', 'e']
true_score = 0
for letter in true_letters:
    true_score += your_name.count(letter) + their_name.count(letter)

love_letters = ['l', 'o', 'v', 'e']
love_score = 0
for letter in love_letters:
    love_score += your_name.count(letter) + their_name.count(letter)

score = int(str(true_score) + str(love_score))
suffix = ""
if (score < 10 or score > 90):
    suffix = ", you go together like coke and mentos"
elif (score < 40 and score > 50):
    suffix = ", you are alright together"

print(f"Your score is {score}{suffix}")
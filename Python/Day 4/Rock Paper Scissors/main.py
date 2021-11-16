import random

rock = '''
    _______
---'   ____)
      (_____)
      (_____)
      (____)
---.__(___)
'''

paper = '''
    _______
---'   ____)____
          ______)
          _______)
         _______)
---.__________)
'''

scissors = '''
    _______
---'   ____)____
          ______)
       __________)
      (____)
---.__(___)
'''

print("Welcome to the Rock Paper Scissors game!")

selected_hand = ""
valid_values = [ "rock", "paper", "scissors"]
while(selected_hand == "" or not (selected_hand.lower() in valid_values)):
    selected_hand = input("Please select either Rock, Paper or Scissors: ")

print(f"You played: {selected_hand}")

computer_hand = valid_values[random.randint(0, 2)]
print(f"The computer played: {computer_hand}")

selected_hand = selected_hand.lower()
if ((selected_hand == "rock" and computer_hand == "scissors") or
    (selected_hand == "paper" and computer_hand == "rock") or
    (selected_hand == "scissors" and computer_hand == "paper")):
    print("You WIN!")
elif ((computer_hand == "rock" and selected_hand == "scissors") or
    (computer_hand == "paper" and selected_hand == "rock") or
    (computer_hand == "scissors" and selected_hand == "paper")):
    print("The computer wins once again")
else:
    print("A draw! No one wins")

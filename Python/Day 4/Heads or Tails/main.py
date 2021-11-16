import random

print("Welcome to Heads or Tails game")
random_value = random.randint(0, 1)
print(f"The coin flip result is: {'Heads' if (random_value == 1) else 'Tails'}")
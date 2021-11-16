import random

print("Welcome to the Banker Roulette, which are today's contestants?")
contestants_string = input("(Insert the names separated by a coma):")
contestants = contestants_string.split(',')

print(f"Today {random.choice(contestants).strip()} pays the bill!")


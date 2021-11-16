import random

print("Welcome to the Banker Roulette, which are today's contestants?")
contestants_string = input("(Insert the names separated by a coma):")
contestants = contestants_string.split(',')

contestant_that_will_pay_index = random.randint(0, len(contestants) - 1)

print(f"Today {contestants[contestant_that_will_pay_index].strip()} pays the bill!")


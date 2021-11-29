import random

def convert_to_int(input):
    try:
        return int(input)
    except ValueError:
        print("Please enter an integer value")
        return ""

letters = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z']
numbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']
symbols = ['!', '#', '$', '%', '&', '(', ')', '*', '+']

print("Welcome to the PyPassword generator")
desired_letters = ""
while desired_letters == "":
    desired_letters = convert_to_int(input("How many letters would you like in your password?"))
    if (desired_letters < 0):
        print("Please specify a positive number")
        desired_letters = ""

desired_symbols = ""
while desired_symbols == "":
    desired_symbols = convert_to_int(input("How many symbols would you like in your password?"))
    if (desired_symbols < 0):
        print("Please specify a positive number")
        desired_symbols = ""
    
desired_numbers = ""
while desired_numbers == "":
    desired_numbers = convert_to_int(input("How many numbers would you like in your password?"))
    if (desired_numbers < 0):
        print("Please specify a positive number")
        desired_numbers = ""

password_characters = desired_letters + desired_symbols + desired_numbers

password_string = ""
letters_in_password = 0
symbols_in_password = 0
numbers_in_password = 0
while len(password_string) < password_characters:
    password_character_set = random.randint(0, 2)
    if (password_character_set == 0 and letters_in_password < desired_letters):
        letters_in_password += 1
        password_string += random.choice(letters)
    if (password_character_set == 1 and symbols_in_password < desired_symbols):
        symbols_in_password += 1
        password_string += random.choice(symbols)
    if (password_character_set == 2 and numbers_in_password < desired_numbers):
        numbers_in_password += 1
        password_string += random.choice(numbers)

print(f"your generated password is: {password_string}")





    

        
def convert_to_int(input):
    try:
        return int(input)
    except ValueError:
        print("Please enter an integer value")
        return ""

def prime_checker(number):
    verification_number = 2
    numbers_that_divide_input = 0
    while numbers_that_divide_input <= 1:        
        if ((number % verification_number) == 0):
            numbers_that_divide_input += 1
        if (number == verification_number):
            break
        verification_number += 1
    return numbers_that_divide_input
    
print("Welcome to the Prime Number Checker")
number_to_check = ""
while number_to_check == "":
    number_to_check = convert_to_int(input("Please input the number desired to check: "))



numbers_that_divide_input = prime_checker(number_to_check)

print(f'The number {number_to_check} {"is" if numbers_that_divide_input <= 1 else "is not"} a prime number')
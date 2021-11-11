def convert_to_int(input):
    try:
        return int(input)
    except ValueError:
        print("Please enter an integer value")
        return ""

number = ""
print("What number do you want to know if it is odd or even?")
while (number == ""):
    number = convert_to_int(input())

if (number % 2 == 0):
    print("The number is even.")
else:
    print("The number is odd.")
def convert_to_int(input):
    try:
        return int(input)
    except ValueError:
        print("Please enter an integer value")
        return ""

print("Which year do you want to check?")

year = ""
while year == "":
    year = convert_to_int(input())

if year % 4 == 0 and (year % 100 != 0 or year % 400 == 0):
    print(year, "is a leap year")
else:
    print(year, "is not a leap year")
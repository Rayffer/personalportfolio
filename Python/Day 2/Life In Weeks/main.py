def convert_to_int(input):
    try:
        return int(input)
    except ValueError:
        print("Please enter an integer value")
        return ""

age = ""
while (age == ""):
    age = convert_to_int(input("How old are you?"))

years_remaining = 90 - age
days = years_remaining * 365 
weeks = years_remaining * 52
months = years_remaining * 12

print(f"You have {days} days left or {weeks} weeks left or {months} months left")


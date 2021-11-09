def convert_to_int(input):
    try:
        return int(input)
    except ValueError:
        print("Please enter an integer value")
        return ""

def convert_to_float(input):
    try:
        return float(input)
    except ValueError:
        print("Please enter a float value")
        return ""

print("Welcome to the tip calculator.")
bill_amount = ""
while (bill_amount == ""):
    bill_amount = convert_to_float(input("What was the total bill in €?"))
people_to_split_bill = ""
while (people_to_split_bill == ""):
    people_to_split_bill = convert_to_int(input("How many people to split the bill?"))
tip_percentage = ""
while (tip_percentage == "" or (tip_percentage != 10 and tip_percentage != 12 and tip_percentage != 15)):
    tip_percentage = convert_to_int(input("What percentage tip would you like to give? 10, 12 or 15?"))
amount_per_person = "{:.2f}".format(bill_amount * (1 + tip_percentage / 100) / people_to_split_bill)
print(f"Each person should pay: {amount_per_person}€")


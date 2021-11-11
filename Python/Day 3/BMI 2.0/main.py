def convert_to_float(input):
    try:
        return float(input)
    except ValueError:
        print("Please enter a float value")
        return ""

height = ""
while(height == ""):
    height = convert_to_float(input("Enter your height in m:"))
weight = ""
while(weight == ""):
    weight = convert_to_float(input("Enter your weight in kg:"))
bmi = round(weight / height ** 2, 2)
print(f"Your bmi is: {bmi}")

if (bmi < 18.5):
    print("You are underweight")
elif (bmi < 25):
    print("You are normal weighted")
elif (bmi < 30):
    print("You are overweight")
elif (bmi < 35):
    print("You are obese")
elif (bmi >= 35):
    print("You are morbidly obese")
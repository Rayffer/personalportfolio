def convert_to_int(input):
    try:
        return int(input)
    except ValueError:
        print("Please enter an integer value")
        return ""

print("Welcome to the High score calculator")
print("Please input a list of scores from 0 to 100 separated by commas:")

list_of_scores = ""
while list_of_scores == "":
    scores = input("").split(",")
    list_of_scores = []
    for score in scores:
        converted_score = convert_to_int(score)
        if (converted_score == ""):
            list_of_scores = ""
            break
        elif (converted_score < 0 or converted_score > 100):
            list_of_scores = ""
            print("Only values between 0 and 100 are accepted")
            break
        else:
            list_of_scores.append(converted_score)

high_score = 0
for score in list_of_scores:
    if (score > high_score):
        high_score = score

print(f"The highest score is: {high_score}")
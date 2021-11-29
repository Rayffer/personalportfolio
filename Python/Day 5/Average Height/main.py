def convert_to_int(input):
    try:
        return int(input)
    except ValueError:
        print("Please enter an integer value")
        return ""

print("Welcome to the average height calculator.")
print("Please input a list of heights in centimeters separated by commas:")
list_of_heights = ""
while list_of_heights == "":
    heights = input("").split(",")
    list_of_heights = []
    for height in heights:
        converted_height = convert_to_int(height)
        if (converted_height == ""):
            list_of_heights = ""
            break
        else:
            list_of_heights.append(converted_height)

sum_of_heights = 0
number_of_students = 0
for height in list_of_heights:
    sum_of_heights += height
    number_of_students += 1

print(f'The average height of the {number_of_students}{"student" if number_of_students == 1 else "students"}  is: {sum_of_heights / number_of_students}')

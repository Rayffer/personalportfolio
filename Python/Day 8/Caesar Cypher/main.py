from art import logo

alphabet = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z']

def caesar(text_to_process: str, shift: int, direction:str):
    result_text = ""
    shift_to_apply = (shift if direction == "encode" else -shift)
    for letter in text_to_process:
        result_text += alphabet[(alphabet.index(letter) + shift_to_apply) % len(alphabet)] if (letter in alphabet) else
    print(f'The {direction}d text is {result_text}')

print(logo)
print("Welcome to the Caesar Cypher")

finished = False
while not finished:
    direction = input("Type 'encode' to encrypt, type 'decode' to decrypt:\n").lower()
    text = input("Type your message:\n").lower()
    shift = int(input("Type the shift number:\n"))

    caesar(text, shift, direction)

    user_response = input("Do you want to go again? type 'yes' to go again").lower()   

    if (user_response != "yes"):
        finished = True


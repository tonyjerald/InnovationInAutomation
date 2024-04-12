import string
import random
def GenerateRandomString(length):
    randomString = ''.join(random.choices(string.ascii_letters, k=length))
    return  randomString
def main(filname):
    # Load the image using pillow
    image = Image.open(f"{filname}.PNG")

    # Convert the image to RGB mode
    image_rgb = image.convert("RGB")

    # Define the specific color you want to find
    target_color = (237, 30, 36)  # Specify the RGB values of the color

    # Get the width and height of the image
    width, height = image.size

    # Find the coordinates of pixels with the specified color
    pixels = []
    for y in range(height):
        for x in range(width):
            pixel_color = image_rgb.getpixel((x, y))
            if pixel_color == target_color:
                pixels.append((x, y))
    first_item_x = ((pixels[:1][0][0])// 10)*10
    first_item_y = ((pixels[:1][0][1])// 10)*10
    last_item_x = ((pixels[-2:][0][0])// 10)*10
    last_item_y = ((pixels[-2:][0][1])// 10)*10

    opencvimage = cv2.imread(f"{filname}.PNG")

    imagecroped = opencvimage[first_item_y:last_item_y,first_item_x:last_item_x]

    #cv2.imshow("test",imagecroped)
    #cv2.waitKey(0)
    randomString = GenerateRandomString(10)
    cv2.imwrite(f"{randomString}.png", imagecroped)
    print(randomString)

if __name__ == "__main__":
    import cv2
    from PIL import Image
    import sys
    argument = sys.argv[1]
    main(argument)

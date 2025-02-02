import pickle
import os
import warnings

warnings.filterwarnings('ignore')

cwd = os.getcwd()

# Load the trained model from the pickle file
with open(os.path.join(cwd, "predictionModelData.pkl"), "rb") as f:
    clf = pickle.load(f)

# Get user input from command line arguments
user_input = input("Enter input data (comma separated): ")
user_input = [int(i) for i in user_input.split(",")]

# Make prediction based on user input
prediction = clf.predict([user_input])

# Print the prediction
print("Prediction:", prediction)
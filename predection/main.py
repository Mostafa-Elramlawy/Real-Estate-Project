import os
import json
import streamlit as st
import pandas as pd
import warnings
import pickle
from sklearn.model_selection import train_test_split
from sklearn import datasets
from sklearn import ensemble
warnings.filterwarnings('ignore')
cwd = os.getcwd()

# Corrected code to read the Excel file
filepath = "D:\predictionModelData.xlsx"
df = pd.read_excel(filepath)
X = df.drop(["price","Address","Title"],axis=1)
Y = df["price"]
X_train, X_test, y_train, y_test = train_test_split(X,Y,test_size=.2, random_state=5)

clf = ensemble.GradientBoostingRegressor(n_estimators=300,max_depth=2,learning_rate=0.15,loss="huber")
clf.fit(X_train,y_train)

# Corrected path for saving the model
with open("predictionModelData.pkl", 'wb') as f:
    pickle.dump(clf, f)
print("accuracy:", clf.score(X_test,y_test)) #model accuracy
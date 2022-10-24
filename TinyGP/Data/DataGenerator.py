# -*- coding: utf-8 -*-
"""data_generator.ipynb

Automatically generated by Colaboratory.

Original file is located at
    https://colab.research.google.com/drive/1oDnlnYNf4IsWcm6acgttmq7xGZgjOs4Q
"""

import numpy as np

def linspace(start, stop, n):
  return np.linspace(start, stop, n)

def f_1(X):
  arr = []
  for x in X:
    y = 5 * x**3 - 2 * x**2 + 3 * x - 17
    arr.append((x, y))
  return np.array(arr)

def f_2(X):
  arr = []
  for x in X:
    y = np.sin(x) + np.cos(x)
    arr.append((x, y))
  return np.array(arr)

def f_3(X):
  arr = []
  for x in X:
    y = 2 * np.log(x + 1)
    arr.append((x, y))
  return np.array(arr)

def f_4(X1, X2):
  arr = []
  for x1 in X1:
    x2 = np.random.choice(X2)
    y = x1 + 2 * x2
    arr.append((x1, x2, y))
  return np.array(arr)

def f_5(X1, X2):
  arr = []
  for x in X1:
    x2 = np.random.choice(X2)
    y = np.sin(x/2) + 2 * np.cos(x2)
    arr.append((x, x2, y))
  return np.array(arr)

def f_6(X1, X2):
  arr = []
  for x1 in X1:
    x2 = np.random.choice(X2)
    y = x1**2 + 3 * x1 * x2 - 7 * x2 + 1
    arr.append((x1, x2, y))
  return np.array(arr)

import math

def exSin(X1):
  arr = []
  for x1 in X1:
    y = np.sin(x1 + math.pi/2)
    arr.append((x1, y))
  return np.array(arr)

def exTan(X1):
  arr = []
  for x1 in X1:
    y = np.tan(2*x1 + 1)
    arr.append((x1, y))
  return np.array(arr)

def exGauss(X1):
  arr = []
  for x1 in X1:
    # f(x) = (1/sqrt(2*3.1415)) * exp (-(x*x)/2)
    y = (1/math.sqrt(2*math.pi) * math.exp((-1*x1*x1)/2))
    arr.append((x1, y))
  return np.array(arr)


def save(name, a, b, c, d, e, arr: np.array):
  file = f"pythonData/{name}.dat"
  f = open(file, "w")
  f.write(f"{a} {b} {c} {d} {e}\n")
  for x in arr:
    a = ""
    for i in x:
      a += f"{i} "
    f.write(a + "\n")
  f.close()

"""1. f(x) = 5*x^3 - 2x^2 + 3x - 17 dziedzina: [-10, 10], [0,100], [-1, 1], [-1000, 1000]"""

n = 100
start = -10
stop = 10
X = linspace(start, stop, n)
arr = f_1(X)
save('fun1', 1, 100, -5, 5, n, arr)

start = 0
stop = 100
X = linspace(start, stop, n)
arr = f_1(X)
save('fun1_1', 1, 100, -5, 5, n, arr)

start = -1
stop = 1
X = linspace(start, stop, n)
arr = f_1(X)
save('fun1_2', 1, 100, -5, 5, n, arr)

start = -1000
stop = 1000
X = linspace(start, stop, n)
arr = f_1(X)
save('fun1_3', 1, 100, -5, 5, n, arr)

"""2. f(x) = sin(x) + cos(x) dziedzina: [-3.14, 3.14], [0,7], [0, 100], [-100, 100]"""

start = -3.14
stop = 3.14
X = linspace(start, stop, n)
arr = f_2(X)
save('fun2', 1, 100, -5, 5, n, arr)

start = 0
stop = 7
X = linspace(start, stop, n)
arr = f_2(X)
save('fun2_1', 1, 100, -5, 5, n, arr)

start = 0
stop = 100
X = linspace(start, stop, n)
arr = f_2(X)
save('fun2_2', 1, 100, -5, 5, n, arr)

start = -100
stop = 100
X = linspace(start, stop, n)
arr = f_2(X)
save('fun2_3', 1, 100, -5, 5, n, arr)

"""3. f(x) = 2* ln(x+1) dziedzina: [0,4], [0, 9], [0,99], [0,999]"""

start = 0
stop = 4
X = linspace(start, stop, n)
arr = f_3(X)
save('fun3', 1, 100, -5, 5, n, arr)

start = 0
stop = 9
X = linspace(start, stop, n)
arr = f_3(X)
save('fun3_1', 1, 100, -5, 5, n, arr)

start = 0
stop = 99
X = linspace(start, stop, n)
arr = f_3(X)
save('fun3_2', 1, 100, -5, 5, n, arr)

start = 0
stop = 999
X = linspace(start, stop, n)
arr = f_3(X)
save('fun3_3', 1, 100, -5, 5, n, arr)

"""4. f(x,y) = x + 2*y dziedzina: x i y [0, 1], [-10, 10], [0, 100], [-1000, 1000]"""

start = -10
stop = 10
X1 = linspace(start, stop, n)
X2 = linspace(start, stop, n)
arr = f_4(X1, X2)
save('fun4', 2, 100, -5, 5, n, arr)

start = 0
stop = 100
X1 = linspace(start, stop, n)
X2 = linspace(start, stop, n)
arr = f_4(X1, X2)
save('fun4_1', 2, 100, -5, 5, n, arr)

start = -1
stop = 1
X1 = linspace(start, stop, n)
X2 = linspace(start, stop, n)
arr = f_4(X1, X2)
save('fun4_2', 2, 100, -5, 5, n, arr)

start = -1000
stop = 1000
X1 = linspace(start, stop, n)
X2 = linspace(start, stop, n)
arr = f_4(X1, X2)
save('fun4_3', 2, 100, -5, 5, n, arr)

"""5. f(x, y) = sin(x/2) + 2* cos(y) dziedzina x, y: [-3.14, 3.14], [0,7], [0, 100], [-100, 100]"""

start = -3.14
stop = 3.14
X1 = linspace(start, stop, n)
X2 = linspace(start, stop, n)
arr = f_5(X1, X2)
save('fun5', 2, 100, -5, 5, n, arr)

start = 0
stop = 7
X1 = linspace(start, stop, n)
X2 = linspace(start, stop, n)
arr = f_5(X1, X2)
save('fun5_1', 2, 100, -5, 5, n, arr)

start = 0
stop = 100
X1 = linspace(start, stop, n)
X2 = linspace(start, stop, n)
arr = f_5(X1, X2)
save('fun5_2', 2, 100, -5, 5, n, arr)

start = -100
stop = 100
X1 = linspace(start, stop, n)
X2 = linspace(start, stop, n)
arr = f_5(X1, X2)
save('fun5_3', 2, 100, -5, 5, n, arr)

"""6. f(x,y) = x^2 + 3x*y - 7y + 1 dziedzina x,y: [-10, 10], [0,100], [-1, 1], [-1000, 1000]"""

start = -10
stop = 10
X1 = linspace(start, stop, n)
X2 = linspace(start, stop, n)
arr = f_6(X1, X2)
save('fun6', 2, 100, -5, 5, n, arr)

start = 0
stop = 100
X1 = linspace(start, stop, n)
X2 = linspace(start, stop, n)
arr = f_6(X1, X2)
save('fun6_1', 2, 100, -5, 5, n, arr)

start = -1
stop = 1
X1 = linspace(start, stop, n)
X2 = linspace(start, stop, n)
arr = f_6(X1, X2)
save('fun6_2', 2, 100, -5, 5, n, arr)

start = -1000
stop = 1000
X1 = linspace(start, stop, n)
X2 = linspace(start, stop, n)
arr = f_6(X1, X2)
save('fun6_3', 2, 100, -5, 5, n, arr)

""" Dodatkowo wykonać regresję symboliczną dla funkcji sin(x + 3.141592/2) w dziedzinie sinusa """

start = 0
stop = math.pi*2
X1 = linspace(start, stop, n)
arr = exSin(X1)
save('exSin', 1, 100, -5, 5, n, arr)

""" Dodatkowo wykonać regresję symboliczną dla funkcji tan(2x +1) w dziedzinie tangensa """

start = -math.pi/2
stop = math.pi/2
X1 = linspace(start, stop, n)
arr = exTan(X1)
save('exTan', 1, 100, -5, 5, n, arr)

'''Wykonać regresję symboliczną dla funkcji Gaussa '''

start = -4
stop = 4
X1 = linspace(start, stop, n)
arr = exGauss(X1)
save('exGauss_1', 1, 100, -5, 5, n, arr)

start = 0
stop = 20
X1 = linspace(start, stop, n)
arr = exGauss(X1)
save('exGauss_2', 1, 100, -5, 5, n, arr)
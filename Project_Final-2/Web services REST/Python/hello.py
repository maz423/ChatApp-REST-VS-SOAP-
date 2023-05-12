from flask import Flask
from flask import request, jsonify

app = Flask(__name__)

Storage = {}
'''
sends welcome prompt to caller
'''


@app.route("/")
def welcome():
    msg = "Welcome to chatroom: \n 1. create room \n 2. join room  \n 3. List existing rooms  \n 4. exit "
    return msg


'''
creates a new entry in the storage dictionary.
'''


@app.route("/create", methods=['GET'])  #changed this to post for the project experiment.
def create():
    name = request.args['name']
    res = "Done"
    if name not in Storage:
        Storage[name] = []
       
        return jsonify(res)
    else:

        return jsonify("Room already exists")


'''
sends all keys of dict to caller
'''


@app.route("/list", methods=['GET'])
def list():
    Response = ""
    for i in Storage:
        Response += str(i) + " , "

    return jsonify(Response)


'''
adds a msg to the dict
'''


@app.route("/send", methods=['GET'])
def send():
    name = request.args['name']
    msg = request.args['msg']
    user = request.args['user']
    res = "Sent"

    Storage[name].append(user + " :" + msg)

    return jsonify(res)


'''
gets last element of an entry from dict
'''


@app.route("/getMsg", methods=['GET'])
def getMsg():
    name = request.args['name']
    res = ""

    if len(Storage[name]) != 0:
        res = Storage[name][-1]

    return jsonify(res)


'''
gets an entry list from dict
'''


@app.route("/getMsgs", methods=['GET'])
def getMsgs():
    name = request.args['name']
    res = ""

    if len(Storage[name]) != 0:
        for i in Storage[name]:
            res += i + "\n"

    return res
    
'''
gets count of messages in a chatroom
'''
    
    
@app.route("/count", methods=['GET'])
def count():
    name = request.args['name']
    return str(len(Storage[name]))


'''
checks if key exists in dict
'''


@app.route("/join", methods=['GET'])
def join():
    name = request.args['name']
    res = "OK"
    err = "ERR"

    if name in Storage:
        return res
    else:
        return err

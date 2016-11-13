import threading

from flask import Flask, request, jsonify
from flask_socketio import SocketIO, emit

app = Flask(__name__)
socketio = SocketIO(app, async_mode='threading')

@app.route('/')
def index():
    return jsonify({'foo': 'bar'})

@socketio.on('connect')
def onConnect():
	print('Connected to client!!')

@socketio.on('test')
def test(json):
	print('Received message')

if __name__ == '__main__':
    socketio.run(app)
// service-worker.js
importScripts('https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/3.1.22/signalr.min.js');

let connection;

self.addEventListener('message', (event) => {
    if (event.data.type === 'INIT') {
        connection = new signalR.HubConnectionBuilder()
            .withUrl(event.data.url)
            .withAutomaticReconnect()
            .build();

        connection.start()
            .then(() => console.log('SignalR connected'))
            .catch(err => console.error('Error while establishing SignalR connection: ', err));
    } else if (event.data.type === 'INVOKE') {
        connection.invoke(event.data.methodName, event.data.arg)
            .then(result => self.postMessage({ type: 'RESULT', result }))
            .catch(err => self.postMessage({ type: 'ERROR', error: err }));
    } else if (event.data.type === 'STOP') {
        connection.stop()
            .then(() => console.log('SignalR connection stopped'))
            .catch(err => console.error('Error stopping SignalR connection: ', err));
    }
});

self.addEventListener('install', (event) => {
    self.skipWaiting();
});

self.addEventListener('activate', (event) => {
    event.waitUntil(self.clients.claim());
});

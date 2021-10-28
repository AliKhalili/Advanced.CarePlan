const request = require('supertest');
const app = require('./apis');


request(app)
  .get('/care-plans')
  .expect('Content-Type', /json/)
  .expect('Content-Length', '15')
  .expect(200)
  .end((err, res) => {
    if (err) throw err;
  });
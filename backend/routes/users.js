var express = require('express');
var router = express.Router();
const { registerUser, signinUser } = require('../services/authService');

// User Registre
router.post('/register', async (req, res) => {
  try{
    const user = await registerUser(req.body);
    res.status(201).json({ success: true, user });
  } catch (err) {
    res.status(400).json({ success: false, message: err.message });
  }
});

// User signin
router.post('/sign-in', async (req, res) => {
  try{
    const result = await signinUser(req.body);
    res.json({ success: true, ...result });
  } catch (err) {
    res.status(401).json({ success: false, message: err.message });
  }
});

module.exports = router;

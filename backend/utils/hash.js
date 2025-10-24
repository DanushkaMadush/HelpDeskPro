const bcrypt = require('bcrypt');
const { SALT_ROUNDS } = require('../config');

const hashPassword = async (plain) => {
    return await bcrypt.hash(plain, SALT_ROUNDS);
};

const verifyPassword = async (plain, hash) => {
    return await bcrypt.compare(plain, hash);
};

module.exports = { hashPassword, verifyPassword };
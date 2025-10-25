const UserModel = require('../models/UserModel');

const createUser = async (userData) => {
    const user = new UserModel(userData);
    const savedUser = await user.save();
    return savedUser.toObject();
};

const findByEmail = async (email) => {
    return await UserModel.findOne({ email: email.toLowerCase() }).exec();
};

const findById = async (id) => {
    return await UserModel.findById(id).exec();
};

module.exports = { createUser, findByEmail, findById };
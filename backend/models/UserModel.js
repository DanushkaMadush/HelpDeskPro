const { string, required } = require('joi');
const mongoose = require('mongoose');

const UserSchema = new mongoose.Schema({
    firstName: { type: String, required: true },
    lastName:  { type: String },
    email: { type: String, required: true, unique:true, lowercase: true },
    password: { type: String, required: true },
    epfNo: { type: String, required: true },
    branch: { type: String, required: true },
    department: { type: String, required: true },
    designation: { type: String, required: true },
    contactNo: { type: String, required: true, unique: true },
    isActive: { type: Boolean, required: true, default: true },
    createdBy: { type: String,  require: true },
    updatedBy: { type: String, required: false },
    timestamps: true
});

module.exports = mongoose.model('User', UserSchema);
const mongoose = require('mongoose');

const UserSchema = new mongoose.Schema({
    firstName: { type: String, required: true },
    lastName:  { type: String, required: true },
    email: { type: String, required: true, unique:true, lowercase: true },
    epfNo: { type: String, required: true },
    branchId: { type: String, required: true },
    departmentId: { type: String, required: true },
    contactNo: { type: String, required: true, unique: true },
    isActive: { type: Boolean, required: true, default: true },
    createdBy: { type: String,  require: true },
    updatedBy: { type: String, required: false },
    timestamps: true
});

module.exports = mongoose.model('User', UserSchema);
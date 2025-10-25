const userRepo = require('../repository/userRepository');
const { hashPassword, verifyPassword } = require('../utils/hash');
const { sign } = require('../utils/jwt');

const registerUser = async (data) => {
    const { firstName, lastName, email, password, epfNo, branch, department, designation, contactNo, createdBy } = data;

    const existingUser = await userRepo.findByEmail(email);
    if (existingUser) {
        throw new Error('Email already in use');
    }

    const hashedPassword = await hashPassword(password);

    const newUser = { firstName, lastName, email: email.toLowerCase(), password: hashedPassword, epfNo, branch, department, designation, contactNo, createdBy};

    const savedUser = await userRepo.createUser(newUser);

    return {
        id: savedUser._id,
        firstName: savedUser.firstName,
        lastName: savedUser.lastName,
        email: savedUser.email,
        epfNo: savedUser.epfNo,
        branch: savedUser.branch,
        department: savedUser.department,
        designation: savedUser.designation,
        contactNo: savedUser.contactNo
    };
};

const signinUser = async ({ email, password }) => {
    const user = await userRepo.findByEmail(email);
    if (!user) {
        throw new Error('Invalid email or password');
    }

    const validPassword = await verifyPassword(password, user.password);
    if (!validPassword) {
        throw new Error('Invalid email or password');
    }

    const token = sign({ id: user._id, email: user.email});

    return{
        token
    };
};

module.exports = { registerUser, signinUser };
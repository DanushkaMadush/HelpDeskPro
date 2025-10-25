const Joi = require('joi');

const registerSchema = Joi.object({
    firstName: Joi.string().required(),
    lastName: Joi.string(),
    email: Joi.string().email().required(),
    password: Joi.string().min(6).required(),
    epfNo: Joi.string().required(),
    branch: Joi.string().required(),
    department: Joi.string().required(),
    designation: Joi.string().required(),
    contactNo: Joi.string().required(),
    createdBy: Joi.string(),
    updatedBy: Joi.string()
});

const signInSchema = Joi.object({
    email: Joi.string().email().required(),
    password: Joi.string().required()
});

const validateBody = (schema) => (req, res, next) => {
    const { error, value } = schema.validate(req.body, { stripUnknown: true });

    if (error) return res.status(400).json({ error: error.details[0].message });
    req.validateBody = value;
    next();
};

module.exports = { validateBody, registerSchema, signInSchema };
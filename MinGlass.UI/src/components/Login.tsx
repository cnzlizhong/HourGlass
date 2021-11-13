import { Form, Input, Button, Checkbox, Typography } from 'antd';
import { useHistory } from 'react-router-dom';
import useLogin from '../hooks/useLogin';

const { Text } = Typography;
const Login = () => {
    const { login, isLoggingIn, logInError } = useLogin();
    const history = useHistory();

    const onLoggedIn = () => {
        history.push('/');
    };

    const onSubmit = (values: any) => {
        login(values.email, values.password, onLoggedIn);
    };

    return (
        <div className="h-full px-5">
            <div className="text-4xl py-20 text-center">Login to MinGlass</div>
            <Form
                name="basic"
                initialValues={{ remember: true }}
                wrapperCol={{
                    xl: { offset: 9, span: 6 },
                    lg: { offset: 8, span: 8 },
                    md: { offset: 7, span: 10 },
                    sm: { offset: 2, span: 20 },
                    xs: 24,
                }}
                labelCol={{
                    xl: { offset: 9, span: 6 },
                    lg: { offset: 8, span: 8 },
                    md: { offset: 7, span: 10 },
                    sm: { offset: 2, span: 20 },
                    xs: 24,
                }}
                onFinish={onSubmit}
                autoComplete="off"
                layout="vertical"
                colon={false}
            >
                <Form.Item
                    label="Email"
                    name="email"
                    rules={[{ required: true, message: 'Email cannot be empty!' }]}
                >
                    <Input size="large" />
                </Form.Item>

                <Form.Item
                    label="Password"
                    name="password"
                    rules={[{ required: true, message: 'Password cannot be empty!' }]}
                >
                    <Input.Password size="large" />
                </Form.Item>

                <Form.Item name="remember" valuePropName="checked">
                    <Checkbox>Remember me</Checkbox>
                </Form.Item>

                <Form.Item>
                    <Button
                        type="primary"
                        htmlType="submit"
                        block
                        size="large"
                        loading={isLoggingIn}
                    >
                        Login
                    </Button>
                    <Text type="danger">{logInError}</Text>
                </Form.Item>
            </Form>
        </div>
    );
};

export default Login;

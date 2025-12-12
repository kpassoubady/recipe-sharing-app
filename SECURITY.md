# Security Policy

## Supported Versions

This project is currently in active development. Security updates will be applied to the following versions:

| Version | Supported          |
| ------- | ------------------ |
| 1.0.x   | :white_check_mark: |

## Reporting a Vulnerability

We take the security of our Recipe Sharing App seriously. If you discover a security vulnerability, please follow these steps:

### How to Report

1. **Do Not** open a public issue for security vulnerabilities
2. Email the maintainers directly with details of the vulnerability
3. Include the following information:
   - Description of the vulnerability
   - Steps to reproduce the issue
   - Potential impact
   - Any suggested fixes (if available)

### What to Expect

- **Initial Response**: We aim to acknowledge receipt of your vulnerability report within 48 hours
- **Status Updates**: We will keep you informed about the progress of fixing the vulnerability
- **Resolution**: Once the vulnerability is fixed, we will notify you and publicly disclose the issue (with credit to you, if desired)

## Security Best Practices

When deploying this application, please follow these security best practices:

### API Security

- **HTTPS Only**: Always use HTTPS in production environments
- **CORS Configuration**: Restrict CORS to specific origins, not wildcard (`*`)
- **API Keys**: Implement API authentication/authorization for production
- **Rate Limiting**: Add rate limiting to prevent abuse
- **Input Validation**: All user inputs are validated, but ensure additional validation for production use

### Database Security

- **Connection Strings**: Never commit connection strings with credentials to version control
- **Environment Variables**: Use environment variables or secure vaults for sensitive configuration
- **Access Control**: Restrict database access to only necessary services
- **Backups**: Implement regular database backups with encryption

### Frontend Security

- **Content Security Policy**: Implement CSP headers to prevent XSS attacks
- **Dependency Updates**: Keep all NuGet packages and dependencies up to date
- **Secure Storage**: Avoid storing sensitive data in browser local storage

### Development Security

- **Secrets Management**: Use .NET Secret Manager for development secrets
- **Code Review**: Review all code changes for security implications
- **Dependency Scanning**: Regularly scan dependencies for known vulnerabilities
- **Security Testing**: Perform security testing before production deployment

## Known Security Considerations

This is an educational/demo project and includes the following security considerations for production deployment:

1. **No Authentication**: Currently, there is no user authentication or authorization
2. **Open API**: All endpoints are publicly accessible without authentication
3. **SQLite Database**: SQLite is suitable for development but consider a more robust database for production
4. **File Upload**: If implementing image uploads, ensure proper validation and sanitization
5. **No Rate Limiting**: API endpoints are not rate-limited

## Security Updates

Security updates will be documented in the [CHANGELOG](CHANGELOG.md) and release notes.

## Additional Resources

- [OWASP Top 10](https://owasp.org/www-project-top-ten/)
- [ASP.NET Core Security Documentation](https://docs.microsoft.com/en-us/aspnet/core/security/)
- [.NET Security Best Practices](https://docs.microsoft.com/en-us/dotnet/standard/security/)

---

**Thank you for helping keep Recipe Sharing App secure!**

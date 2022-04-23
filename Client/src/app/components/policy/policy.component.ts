import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
@Component({
  selector: 'app-policy',
  templateUrl: './policy.component.html',
  styleUrls: ['./policy.component.scss']
})
export class PolicyComponent implements OnInit {
  longText = '';
  policies = [
    {
      id: 1,
      title: 'Privacy Policy',
      content: `
      <p>
        This Privacy Policy governs the manner in which <a href="https://www.example.com">www.example.com</a> collects, uses, maintains and discloses information collected from users (each, a "User") of the <a href="https://www.example.com">www.example.com</a> website ("Site"). This privacy policy applies to the Site and all products and services offered by <a href="https://www.example.com">www.example.com</a>.
      </p>
      <p>
        <strong>Personal identification information</strong>
      </p>
      <p>
        We may collect personal identification information from Users in a variety of ways, including, but not limited to, when Users visit our site, register on the site, and in connection with other activities, services, features or resources we make available on our Site. Users may be asked for, as appropriate, name, email address. Users may, however, visit our Site anonymously. We will collect personal identification information from Users only if they voluntarily submit such information to us. Users can always refuse to supply personally identification information, except that it may prevent them from engaging in certain Site related activities.
      </p>
      <p>
        <strong>Non-personal identification information</strong>
      </p>
      <p>
        We may collect non-personal identification information about Users whenever they interact with our Site. Non-personal identification information may include the browser name, the type of computer and technical information about Users means of connection to our Site, such as the operating system and the Internet service providers utilized and other similar information.
      </p>
      <p>
        <strong>Web browser cookies</strong>
      </p>  <p>
        Our Site may use "cookies" to enhance User experience. User's web browser places cookies on their hard drive for record-keeping purposes and sometimes to track information about them. User may choose to set their web browser to refuse cookies, or to alert you when cookies are being sent. If they do so, note that some parts of the Site may not function properly.
      </p>
      <p>
        <strong>How we use collected information</strong>
      </p>
      <p>
        <a href="https://www.example.com">www.example.com</a> may collect and use Users personal information for the following purposes:
      </p>
      <ul>
        <li>
          <i>To run and operate our Site</i><br/>
          We may need your information display content on the Site correctly.
        </li>
        <li>
          <i>To improve customer service</i><br/>
          Information you provide helps us respond to your customer service requests and support needs more efficiently.
        </li>
        <li>
          <i>To personalize user experience</i><br/>
          We may use information in the aggregate to understand how our Users as a group use the services and resources provided on our Site.
        </li>
        <li>
          <i>To improve our Site</i><br/>
          We may use feedback you provide to improve our products and services. 
        </li>
        <li>
          <i>To send periodic emails</i><br/>
          We may use the email address to send User information and updates pertaining to their order. It may also be used to respond to their inquiries, questions, and/or other requests. If User decides to opt-in to our mailing list, they will receive emails that may include company news, updates, related product or service information, etc. If at any time the User would like to unsubscribe from receiving future emails, we include detailed unsubscribe instructions at the bottom of each email or User may contact us via our Site.
        </li>
      </ul>
      <p>
        <strong>How we protect your information</strong>
      </p>
      <p>
        We adopt appropriate data collection, storage and processing practices and security measures to protect against unauthorized access, alteration, disclosure or destruction of your personal information, username, password, transaction information and data stored on our Site.
      </p>
      <p>
        <strong>Sharing your personal information</strong>
      </p>
      <p>
        We do not sell, trade, or rent Users personal identification information to others. We may share generic aggregated demographic information not linked to any personal identification information regarding visitors and users with our business partners, trusted affiliates and advertisers for the purposes outlined above.
      </p>
      <p>
        <strong>Third party websites</strong>
      </p>
      <p>
        Users may find advertising or other content on our Site that link to the sites and services of our partners, suppliers, advertisers, sponsors, licensors and other third parties. We do not control the content or links that appear on these sites and are not responsible for the practices employed by websites linked to or from our Site. In addition, these sites or services, including their content and links, may be constantly changing. These sites and services may have their own privacy policies and customer service policies. Browsing and interaction on any other website, including websites which have a link to our Site, is subject to that website's own terms and policies.
      </p>
      <p>
        <strong>Changes to this privacy policy</strong>
      </p>
      <p>
        <a href="https://www.example.com">www.example.com</a> has the discretion to update this privacy policy at any time. When we do, we will post a notification on the main page of our Site, revise the updated date at the bottom of this page and send you an email. We encourage Users to frequently check this page for any changes to stay informed about how we are helping to protect the personal information we collect. You acknowledge and agree that it is your responsibility to review this privacy policy periodically and become aware of modifications.
      </p>
      <p>
        <strong>Your acceptance of these terms</strong>
      </p>
      <p>
        By using this Site, you signify your acceptance of this policy. If you do not agree to this policy, please do not use our Site. Your continued use of the Site following the posting of changes to this policy will be deemed your acceptance of those changes.
      </p>
      <p>
        <strong>Contacting us</strong>
      </p>
      <p>
        If you have any questions about this Privacy Policy, the practices of this site, or your dealings with this site, please contact us at:
      </p>
      <p>
        <a href="https://www.example.com">www.example.com</a><br/>
        <a href="mailto:    ">    </a>
      </p>` 
    },
    {
      id: 2,
      title: 'Terms and Conditions',
      content: `
      <p>
        Welcome to <a href="https://www.example.com">www.example.com</a> (“<strong>Site</strong>”). These Terms of Use (“<strong>Terms</strong>”) govern your use of the Site and provide details about your legal rights and obligations. Please read these Terms carefully before using the Site. By accessing or using the Site in any manner, you agree to be bound by these Terms. If you do not agree to all the terms of this agreement, you may not access the Site or use any services. If you do not have the authority to bind the Company in any respect, you do not have the authority to use the Site.
      </p>
      <p>
        <strong>Terms of Use</strong>
      </p> 
      <p>
        <strong>1. General</strong>
      </p>
      <p>
        <strong>1.1. Site</strong>
      </p>
      <p>
        The Site is owned and operated by <a href="https://www.example.com">www.example.com</a> (“<strong>Company</strong>”). The Site is operated from our offices in <strong>New York, New York</strong>.
      </p>
      <p>
        <strong>1.2. Content</strong>
      </p>
      <p>
        Our Site contains content that is owned by or licensed to us. Some of the content on our Site, such as text, graphics, logos, button icons, images, and photography, are protected by copyright and trade mark law. The compilation of all content available on our Site is the exclusive property of Company.
      </p>
      <p>
        <strong>1.3. License</strong>
      </p>
      <p>
        Unless otherwise stated, the Site is licensed to you under the <a href="https://www.example.com">www.example.com</a> license.
      </p>
      <p> 
        <strong>1.4. Copyright</strong>
      </p>
      <p>
        All rights reserved.
      </p>
      <p>
        <strong>1.5. Third Party Content</strong>
      </p>
      <p>
        Our Site may contain content that is contributed by third parties, including other users of our Site. Such third party content is the property of the third party and is protected by copyright, trademark, and other intellectual property or proprietary rights.
      </p>
      <p>
        <strong>1.6. Modifications</strong>
      </p>
      <p>
        We reserve the right to modify or remove the Site from time to time, and we will make a good faith effort to provide notice of such modifications or removals to you.
      </p>
      <p>
        <strong>1.7. No Warranty</strong>
      </p>
      <p>
        You expressly understand and agree that your use of the Site is at your sole risk. The Site is provided on an "as is" and "as available" basis. We expressly disclaim all warranties of any kind, whether express or implied, including, but not limited to, implied warranties of merchantability, fitness for a particular purpose, title, and non-infringement.
      </p>
      <p>
        <strong>1.8. Limitation of Liability</strong>
      </p>
      <p>
        In no event shall Company, nor its officers, directors, employees, affiliates, agents, or partners, be liable for any indirect, incidental, special, consequential or punitive damages whatsoever (including, without limitation, damages for loss of profits, goodwill, use, data or other intangible losses) arising out of the use of or inability to use the Site, even if Company or any of its officers, directors, employees, affiliates, agents, partners, or suppliers has been advised of the possibility of such damages.
      </p>
      <p>
        <strong>1.9. Indemnification</strong>
      </p>
      <p>
        You agree to indemnify, defend and hold harmless Company and its officers, directors, employees, affiliates, agents, partners, and suppliers, and each of their respective officers, directors, employees, affiliates, agents, partners, and suppliers, from and against any and all claims, liabilities, damages, judgments, awards, penalties, costs and expenses (including reasonable attorneys' fees) arising out of or related to your use of the Site or violation of these Terms.
      </p>
      <p>
        <strong>1.10. Governing Law</strong>
      </p>
      <p>
        These Terms shall be governed and construed in accordance with the laws of the State of New York, without regard to its conflict of law provisions.
      </p>
      <p>
        Our failure to enforce any right or provision of these Terms will not be considered a waiver of those rights. If any provision of these Terms is held to be invalid or unenforceable by a court, the remaining provisions of these Terms will remain in effect. These Terms constitute the entire agreement between us regarding our Service, and supersede and replace any prior agreements we might have between us regarding the Service.
      </p>
      <p>
        <strong>1.11. Changes to Terms</strong>
      </p>
      <p>
        We reserve the right, at our sole discretion, to modify or replace these Terms at any time. If a revision is material we will try to provide at least 30 days notice prior to any new
        terms taking effect. What constitutes a material change will be determined at our sole discretion.
      </p>
      <p>
        <strong>1.12. Contact Us</strong>
      </p>
      <p>
        If you have any questions about these Terms, please contact us at:
      </p>
      <p>
        <a href="https://www.example.com">www.example.com</a><br/>
        <a href="mailto:    ">    </a>
      </p>`
    }
  ]

  constructor(
    private titleService: Title
  ) { 
    this.titleService.setTitle('Terms and Conditions');
  }

  ngOnInit(): void {
  }

}
